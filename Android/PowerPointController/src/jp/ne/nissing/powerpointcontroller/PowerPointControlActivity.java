package jp.ne.nissing.powerpointcontroller;

import java.io.*;
import java.net.*;
import java.util.*;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.SharedPreferences;
import android.graphics.*;
import android.os.*;
import android.support.v4.app.ActionBarDrawerToggle;
import android.support.v4.widget.DrawerLayout;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.*;
import android.widget.AdapterView.OnItemClickListener;

import jp.ne.nissing.powerpointcontroller.R.id;
import jp.ne.nissing.powerpointcontroller.adapter.SlideListAdapter;
import jp.ne.nissing.powerpointcontroller.manager.ImageCache;
import jp.ne.nissing.powerpointcontroller.util.TCPCommands;
import jp.ne.nissing.powerpointcontroller.util.TCPConnector;

public class PowerPointControlActivity extends Activity {

    private static final int MENU_ID_END = (Menu.FIRST + 1);
    private static final int MENU_ID_QUIT = (Menu.FIRST + 2);
    private static final int MENU_ID_RESTART = (Menu.FIRST + 3);
    private static final int MENU_ID_ADDRESS = (Menu.FIRST + 4);
    private static final String ADDRESS_KEY = "address";
    private static final String ADDESS_PREF = "PowerPointController";
    
    private Context context = null;
    SharedPreferences pref;
    SharedPreferences.Editor editor;
    
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_power_point_control);
        context = this;
        
        Button startButton = (Button) findViewById(id.startButton);
        Button nextButton = (Button) findViewById(id.nextButton);
        Button previousButton = (Button) findViewById(id.previousButton);
//        Button loadSlidesButton = (Button) findViewById(id.loadSlideButton);
        
        pref = getSharedPreferences(ADDESS_PREF, Activity.MODE_PRIVATE);
        
        TCPConnector.SetIPAddress(pref.getString(ADDRESS_KEY, "127.0.0.1"));
        
        startButton.setOnClickListener(new OnClickListener() {
            
            @Override
            public void onClick(View v) {
                TCPConnector.sendCommand(context, TCPCommands.START);
            }
        });
        
        nextButton.setOnClickListener(new OnClickListener() {
            
            @Override
            public void onClick(View v) {
                TCPConnector.sendCommand(context, TCPCommands.NEXT);
            }
        });
        
        previousButton.setOnClickListener(new OnClickListener() {
            
            @Override
            public void onClick(View v) {
                TCPConnector.sendCommand(context, TCPCommands.PREVIOUS);
            }
        });
        
        DrawerLayout drawerLayout = (DrawerLayout) findViewById(R.id.drawer_layout);
        ActionBarDrawerToggle drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, R.drawable.ic_drawer, R.string.open, R.string.close){
        	@Override
        	public void onDrawerOpened(View drawerView) {
        		super.onDrawerOpened(drawerView);

				AsyncTask<Void, Void, Integer> loadImageTask = new AsyncTask<Void, Void, Integer>(){
			        
					@Override
					protected Integer doInBackground(Void... params) {
						synchronized (context) {

							String count = "-1";
							Socket socket = null;
							try {
								socket = new Socket(TCPConnector.getIPAddress(),TCPConnector.getPort());
								PrintWriter pw = new PrintWriter(socket.getOutputStream(), true);
								pw.println("ppc_load_count");
								
								int size;
								byte[] tmp = new byte[1024];
								BufferedInputStream in = new BufferedInputStream(socket.getInputStream());
								size = in.read(tmp);
								count = new String(tmp,0,size, "UTF-8");


								if(in != null){
									in.close();
								}
								if(pw != null){
									pw.close();
								}
								if(socket != null){
									socket.close();
								}

							} catch (UnknownHostException e) {
								e.printStackTrace();
							} catch (IOException e) {
								e.printStackTrace();
							}

							return Integer.parseInt(count);
						}
					}

					@Override
					protected void onPostExecute(Integer result) {
						super.onPostExecute(result);
						if(result != null && result > 0){
							ListView listView = (ListView) findViewById(R.id.slide_list);
					        ArrayList<Map<String, Object>> list_data = new ArrayList<Map<String, Object>>();
					        
					        for(int i = 0; i < result;i++){
					            HashMap<String, Object> map = new HashMap<String, Object>();
					            map.put("slide", i);
					            list_data.add(map);
					        }
					        String[] from_template = {};
					        int[] to_template = {};
							SlideListAdapter adapter = new SlideListAdapter(context, list_data, R.layout.slide, from_template, to_template);
							listView.setAdapter(adapter);
							listView.setOnItemClickListener(new OnItemClickListener() {

								@Override
								public void onItemClick(AdapterView<?> arg0,
										View arg1, int arg2, long arg3) {
									int index = arg2 + 1;
									TCPConnector.sendCommand(context, "ppc_jump "+index);
								}
								
							});
						}
					}

				};

				loadImageTask.execute();
        	}
        };
        drawerLayout.setDrawerListener(drawerToggle);
    
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
//        getMenuInflater().inflate(R.menu.power_point_control, menu);
        
        menu.add(Menu.NONE,MENU_ID_ADDRESS,Menu.NONE,R.string.address);
        menu.add(Menu.NONE,MENU_ID_RESTART,Menu.NONE,R.string.restart);
        menu.add(Menu.NONE,MENU_ID_END,Menu.NONE,R.string.end);
        menu.add(Menu.NONE,MENU_ID_QUIT,Menu.NONE,R.string.quit);
        return super.onCreateOptionsMenu(menu);
    }
    
    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        
        switch (item.getItemId()) {
        case MENU_ID_END:
            TCPConnector.sendCommand(context, TCPCommands.END);
            break;
        case MENU_ID_QUIT:
            TCPConnector.sendCommand(context, TCPCommands.QUIT);
            break;
        case MENU_ID_RESTART:
            TCPConnector.sendCommand(context, TCPCommands.RESTART);
            break;
        case MENU_ID_ADDRESS:
            final EditText editText = new EditText(context);
            editText.setText(pref.getString(ADDRESS_KEY, "127.0.0.1"));
            
            new AlertDialog.Builder(context).setIcon(android.R.drawable.ic_dialog_info)
            .setTitle(R.string.address).setView(editText).setPositiveButton(android.R.string.ok, new DialogInterface.OnClickListener() {
                
                @Override
                public void onClick(DialogInterface dialog, int which) {
                    editor = pref.edit();
                    editor.putString(ADDRESS_KEY, editText.getText().toString());
                    editor.commit();
                    TCPConnector.SetIPAddress(editText.getText().toString());
                }
            })
            .setNegativeButton(android.R.string.cancel, new DialogInterface.OnClickListener() {
                
                @Override
                public void onClick(DialogInterface dialog, int which) {
                }
            }).show();
            break;
        default:
            break;
        }
        return super.onOptionsItemSelected(item);
    }

}

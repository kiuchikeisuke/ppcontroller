package jp.ne.nissing.powerpointcontroller;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import jp.ne.nissing.powerpointcontroller.R.id;
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
            .setTitle(R.string.address).setView(editText).setPositiveButton("OK", new DialogInterface.OnClickListener() {
                
                @Override
                public void onClick(DialogInterface dialog, int which) {
                    editor = pref.edit();
                    editor.putString(ADDRESS_KEY, editText.getText().toString());
                    editor.commit();
                    TCPConnector.SetIPAddress(editText.getText().toString());
                }
            })
            .setNegativeButton("キャンセル", new DialogInterface.OnClickListener() {
                
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

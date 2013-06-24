package jp.ne.nissing.powerpointcontroller.adapter;

import java.io.*;
import java.net.*;
import java.util.*;

import jp.ne.nissing.powerpointcontroller.R;
import jp.ne.nissing.powerpointcontroller.manager.ImageCache;
import jp.ne.nissing.powerpointcontroller.util.TCPConnector;

import android.R.anim;
import android.content.Context;
import android.graphics.*;
import android.os.AsyncTask;
import android.util.Log;
import android.view.*;
import android.widget.*;

public class SlideListAdapter extends SimpleAdapter{

	private Context context;
	private LayoutInflater mInflater;
	
	public SlideListAdapter(Context context,
			List<? extends Map<String, Object>> data, int resource, String[] from,
			int[] to) {
		super(context, data, resource, from, to);
		
		this.context = context;
		
		this.mInflater = (LayoutInflater) context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
	}
	
	@Override
	public View getView(final int position, View convertView, ViewGroup parent) {
		View v = convertView;
		
		if(v == null){
			v = mInflater.inflate(R.layout.slide, null);
		}
				
		final ImageView slideImageView = (ImageView) v.findViewById(R.id.slide_image);
		final ProgressBar progress = (ProgressBar) v.findViewById(R.id.wait_bar);
		
		if(ImageCache.getCache(position) != null){
			slideImageView.setImageBitmap(ImageCache.getCache(position));
			progress.setVisibility(View.GONE);
			slideImageView.setVisibility(View.VISIBLE);
		}
		else{
	
			AsyncTask<Integer, Void, Bitmap> loadImageTask = new AsyncTask<Integer, Void, Bitmap>(){

				@Override
				protected Bitmap doInBackground(Integer... params) {
					synchronized (context) {

						Bitmap retval = null;
						int index = params[0] + 1;
						Socket socket = null;
						try {
							socket = new Socket(TCPConnector.getIPAddress(),TCPConnector.getPort());
							PrintWriter pw = new PrintWriter(socket.getOutputStream(), true);
							pw.println("ppc_load_slide "+index);

							InputStream in = socket.getInputStream();
							ByteArrayOutputStream bout = new ByteArrayOutputStream();
							byte[] bytes = new byte[1024];
							while(true){
								int len = in.read(bytes);
								if(len < 0){
									Log.d("PPC", "call break");
									break;
								}
								Log.d("PPC", "call set byte array"+len);
								bout.write(bytes, 0, len);
								if(len < 1024){
									Log.d("PPC", "call break 2nd");
									break;
								}
							}
							Log.d("PPC", "call end read stream");
							byte[] imageBytes = bout.toByteArray();

							retval = BitmapFactory.decodeByteArray(imageBytes,0,imageBytes.length);
							Log.d("PPC", "call end decode byte array");

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

						return retval;
					}
				}

				@Override
				protected void onPostExecute(Bitmap result) {
					super.onPostExecute(result);
					if(result != null){
						slideImageView.setImageBitmap(result);
						ImageCache.setCache(position, result);
					}
					else{
						slideImageView.setImageDrawable(context.getResources().getDrawable(R.drawable.ic_launcher));
					}
					progress.setVisibility(View.GONE);
					slideImageView.setVisibility(View.VISIBLE);
				}

			};

			loadImageTask.execute(position);
		}
		
		return v;
	}

}

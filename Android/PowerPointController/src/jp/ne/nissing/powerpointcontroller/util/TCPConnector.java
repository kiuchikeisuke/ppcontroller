package jp.ne.nissing.powerpointcontroller.util;

import android.content.Context;
import android.os.AsyncTask;
import android.widget.Toast;

import java.io.IOException;
import java.io.PrintWriter;
import java.net.Socket;
import java.net.UnknownHostException;


public class TCPConnector {
    private static final int PORT = 12344;
    private static String IPAddress = "127.0.0.1";
    
    public static void SetIPAddress(String value){
        IPAddress = value;
    }
    public static String getIPAddress(){
        return IPAddress;
    }
    
    public static int getPort(){
    	return PORT;
    }
    
    public static void sendCommand(final Context context, final TCPCommands command){
        sendCommand(context, command.toString());
   }
    
    public static void sendCommand(final Context context, final String cmd){
    	        AsyncTask<String, Void, Boolean> task = new AsyncTask<String, Void, Boolean>(){

            @Override
            protected Boolean doInBackground(String... params) {
                String cmd = params[0];
                
                Socket socket = null;
                boolean isSuccess = true;
                try {
                    socket = new Socket(IPAddress, PORT);
                    PrintWriter pw = new PrintWriter(socket.getOutputStream(), true);
                    pw.println(cmd.toString());
                } catch (UnknownHostException e) {
                    e.printStackTrace();
                    isSuccess = false;
                } catch (IOException e) {
                    e.printStackTrace();
                    isSuccess = false;
                }
                
                if(socket != null){
                    try {
                        socket.close();
                    } catch (IOException e) {
                        e.printStackTrace();
                        isSuccess = false;
                    }
                }
                
                return isSuccess;
            }
            
            @Override
            protected void onPostExecute(Boolean result) {
                if(result){
                    Toast.makeText(context, "success "+cmd, Toast.LENGTH_SHORT).show();
                }
                else{
                    Toast.makeText(context, "failed "+cmd, Toast.LENGTH_SHORT).show();
                }
            }
        };
        task.execute(cmd);
    }
    
}

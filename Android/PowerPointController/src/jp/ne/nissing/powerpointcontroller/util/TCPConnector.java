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
    
    public static void sendCommand(final Context context, final TCPCommands command){
        
        AsyncTask<TCPCommands, Void, Boolean> task = new AsyncTask<TCPCommands, Void, Boolean>(){

            @Override
            protected Boolean doInBackground(TCPCommands... params) {
                TCPCommands cmd = params[0];
                
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
                    Toast.makeText(context, "success "+command, Toast.LENGTH_SHORT).show();
                }
                else{
                    Toast.makeText(context, "failed "+command, Toast.LENGTH_SHORT).show();
                }
            }
        };
        task.execute(command);
        
   }
    
}

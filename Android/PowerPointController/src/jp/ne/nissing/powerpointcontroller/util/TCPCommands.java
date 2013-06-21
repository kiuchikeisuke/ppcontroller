package jp.ne.nissing.powerpointcontroller.util;

public enum TCPCommands {
    START("ppc_start"),
    END("ppc_end"),
    NEXT("ppc_next"),
    PREVIOUS("ppc_previous"),
    RESTART("ppc_restart"),
    QUIT("ppc_quit");
    
    private final String value;
    
    private TCPCommands(String value){
        this.value = value;
    }
    
    @Override
    public String toString() {
        return this.value;
    }
}

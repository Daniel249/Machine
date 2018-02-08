using System;
using System.Collections.Generic;
using System.Linq;

// TODO add method: if requires check addComms, 
// if no coincidence call getHelp(). apply to all commands
class Command {
    public string name;
    protected string help;
    protected Tuple[] comms = new Tuple[0]; //list comm letter
    //protected string[] commStr = new string[0]; //list comm help
    protected bool requires = false;
    
    //virtual method, call with base.metodo()
    // base.metodo checks if help is needed before running local metodo
    public virtual bool metodo(machine mach, params string[] added) {
        //if needs extra input or -h: getHelp
        if(this.requires) {
            if(checkArray(added)) {
                Console.WriteLine("Use -h for help on command");
                Console.WriteLine();
                return false;
            }
        }
        foreach(string str in added) {
            if(str == "-h") {
                getHelp();
                return false;
            }
        }
        return true;
    }
    // print help
    protected void getHelp() {
        Console.WriteLine(this.help);
        getComms();
    }
    void getComms() {
        for(int i = 0; i < comms.Length; i++) {
            Console.WriteLine(comms[i].Item1 + "\t"+"\t" + comms[i].Item2);
        }
        Console.WriteLine();
    }
    // check if array (normally addComms) is empty
    bool checkArray(string[] arr) {
        if(arr.Length < 1) {
            return true;
        }
        if(arr[0] == "" || arr[0] == " ") {
            return true;
        }
        foreach(string str in arr) {
            foreach(Tuple tup in this.comms) {
                string fstr = tup.Item1;
                if(str == fstr) {
                    return false;
                }
            }
        }
        return true;
    }
}



// holds all commands for a machine
class CommList {
    List<Command> comms;
    public CommList(){
        comms = new List<Command>();
    }
    //check if string is in CommandList; return Command; return addComms
    public bool isCommand(string str, out Command possibleComm,
     out string[] addComms) {
        string[] tempSplit = str.Split(' '); //input to string[]
        string possibleStr = tempSplit.First(); //get Command
        addComms = tempSplit.Skip(1).ToArray(); //get extras
        foreach(Command comm in comms) {
            //if match with command found run it
            if(comm.name == possibleStr) {
                possibleComm = comm;
                return true;
            }
        }
        //TODO return command to ask again
        possibleComm = null;
        return false;
    }
    // for listc
    public void listComms(machine mach) {
        mach.respond("All commands available:");
        foreach(Command com in comms) {
            mach.respond(com.name);
        }
        mach.respond("For more info, -h");
    }
    // used in machine constructor
    public void addComm(Command comm) {
        comms.Add(comm);
    }
}
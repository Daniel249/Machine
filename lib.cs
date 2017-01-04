using System;
using System.Collections.Generic;
using System.Linq;

// List<Tuple> used as memory on machine
class TupleList {
    private List<Tuple> reg = new List<Tuple>();
    
    public int getLength() {
        return reg.Count;
    }
    // add to memory; if only message, emisor unknown
    public void addReg(params string[] info) {
        // 2 or 1 strings expected
        string who;
        string what;
        if(info.Length < 2) {
            who = "UNKNOWN";
            what = info[0];
        } else {
            who = info[0];
            what = info[1];
        }
        reg.Add(new Tuple(who, what));
    }
    // show from memory. exact location. from, to. all
    public void printReg(string msg) {
        Console.WriteLine(msg);
        foreach (Tuple tuple in reg) {
            Console.WriteLine(tuple.Item1 + "\t \t" + tuple.Item2);
        }
        Console.WriteLine();
    }
    public void printReg(int x) {
        if(x > reg.Count) {
            //no existe
            Console.WriteLine("Memory location not found");
            Console.WriteLine();
            return;
        }
        // Tuple tuple = reg[x];
        Console.WriteLine("Memory location {0}", (x));
        Console.WriteLine(reg[x].Item1 + "\t \t" + reg[x].Item2);
    }
    public void printReg(int st, int fin) {
        if(st < 0) {
            st = 0;
        }
        if(st >= reg.Count) {
            Console.WriteLine("Start location does not exist");
            Console.WriteLine("Total memory used: {0}", reg.Count);
            return;
        }
        if(fin >= reg.Count) {
            fin = reg.Count-1;
            Console.WriteLine("End location set to last memory index: {0}", fin);           
        }
        for(; st<= fin; st++) {
            Console.WriteLine(reg[st].Item1 + "\t" + "\t" + reg[st].Item2);
        }
        Console.WriteLine();

    }
    /*public static void Main(string[] args) {
        TupleList tupla  = new TupleList();
        for(int i = 0; i < 4; i++) {
            tupla.addReg("user", "hola");
            tupla.addReg("mensaje");
        }
        tupla.printReg(10);
        tupla.printReg(3, 6);
        tupla.printReg(3,10);
        tupla.printReg();    
        tupla.printReg(10,12);    
    }*/
}
// hold 2 strings
class Tuple {
    // used in CommandList(extraCommand, function) and Memory(emisor, message)
    public string Item1;
    public string Item2;
    public Tuple(string Item1, string Item2) {
        this.Item1 = Item1;
        this.Item2 = Item2;
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
    //check if array normally addComms is empty
    bool checkArray(string[] arr) {
        if(arr.Length < 1) {
            return true;
        }
        if(arr[0] == "" || arr[0] == " ") {
            return true;
        }
        return false;
    }
}
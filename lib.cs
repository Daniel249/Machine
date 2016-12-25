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
    public void printReg() {
        Console.WriteLine("Show memory...");
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
        Console.WriteLine("Memory location {0}", (x-1));
        Console.WriteLine(reg[x].Item1 + "\t \t" + reg[x].Item2);
    }
    public void printReg(int st, int fin) {
        if(st < 0) {
            st = 0;
        }
        if(st >= reg.Count) {
            Console.WriteLine("Start location does not exist");
            Console.WriteLine("Total memory used: {0}", reg.Count);
            Console.WriteLine();
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
    public bool isCommand(string str, out Command possibleComm, out string[] addComms) {
        string[] tempSplit = str.Split(' '); //input to string[]
        string possibleStr = tempSplit.First(); //get Command
        addComms = tempSplit.Skip(1).ToArray(); //get extras
        foreach(Command comm in comms){
            if(comm.name == possibleStr) {
                //if match found
                possibleComm = comm;
                return true;
            }
        }
        //TODO return command to ask again
        possibleComm = null;
        return false;
    }
    public void addComm(Command comm) {
        comms.Add(comm);
    }
}
class Command {
    public string name;
    protected string help;
    protected Tuple[] comms = new Tuple[0]; //list comm letter
    //protected string[] commStr = new string[0]; //list comm help
    protected bool requires = false;
    
    //virtual method, call with base.metodo()
    public virtual bool metodo(machine mach, params string[] added) {
        //if needs extra input or -h: getHelp
        if(this.requires && added.Length == 0) {
            getHelp();
            return false;
        }
        foreach(string str in added) {
            if(str == "-h") {
                getHelp();
                return false;
            }
        }
        return true;
    }
    protected void getHelp() {
        Console.WriteLine(this.help);
        getComms();
    }
    void getComms() {
        for(int i = 0; i < comms.Length; i++) {
            Console.WriteLine(comms[i].Item1 + "\t"+"\t" + comms[i].Item2);
        }
    }

}
// declared :Commands
class factorial : Command {
    public factorial() {
        name = "factorial";
        help = "int as input, returns factorial";
        //comms = 
    }
    public override bool metodo(machine mach, params string[] addComms) {
        if(!base.metodo(mach, addComms)) {
            return false;
        }
        mach.respond("Escribe un numero natural");
        int num = mach.askNumber();
        int res = 1;
        for(int i = 2; i < num; i++){
            res = res * i;
        }
        mach.respond("Answer: " + res.ToString());
        return true;
    }
}
class showMemory : Command {
    public showMemory() {
        name = "showmem";
        help = "shows Memory";
        requires = true;
        comms = new Tuple[] {
            new Tuple("-a", "show all memory"),
            new Tuple("-l", "show last 4 memory spots")
        };
    }
    public override bool metodo(machine mach, 
    params string[] addComms) {
        if(!base.metodo(mach, addComms)) {
            return false;
        }
        foreach(string possibleComm in addComms) {
            if(possibleComm == "-l") {
                int fn = mach.memory().getLength() - 1;
                int st = fn - 3;
                mach.memory().printReg(st, fn);
                return true;
            } else if(possibleComm == "-a") {
                mach.memory().printReg();
                return true;
            }
        }
        if(addComms.Length > 2){
            mach.respond("at most 2 inputs");
        } else if(addComms.Length == 2) {
            int st; bool 
            bul = int.TryParse(addComms[0], out st);
            int fn; 
            bool bol = int.TryParse(addComms[1], out fn);
            if(bul && bol) {
                mach.memory().printReg(st, fn);
            } else {
                mach.respond("not valid combination");
            }
        } else {
            int st; 
            if(int.TryParse(addComms[0], out st)) {
                mach.memory().printReg(st);
            }
        }
        return true;
    }
}
// end environment if mach.isAdmin
class exit : Command {
    public exit() {
        name = "exit";
        help = "Ends application";
        requires = false;
        comms = new Tuple[] {
            new Tuple("-m", "add comment"),
            new Tuple("-a", "Ends all environments"),
        };
    }
    public override bool metodo(machine mach, 
    params string[] addComms) {
        if(!base.metodo(mach, addComms)) {
            return false;
        }
        string msg = "ended";
        string enviro = "Environment";
        if(addComms.Length == 0) {
            return mach.endEnvironment(enviro, msg);
        }
        for(int i = 0; i < addComms.Length; i++) {
            switch(addComms[i]) {
                case "-m":
                    msg = addComms[i+1];
                    break;
                case "-f":
                    // force close
                    break;
                case "-a":
                    // close all environments
                    // TODO check superAdmin
                    break;
                default:
                    break;
            }
        }

        return mach.endEnvironment(enviro, msg);
    }
}
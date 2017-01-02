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
    public void listComms(machine mach) {
        mach.respond("All commands available:");
        foreach(Command com in comms) {
            mach.respond(com.name);
        }
        mach.respond("For more info, -h");
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
        if(this.requires) {
            if( added.Length < 1) {
                Console.WriteLine("Use -h for help on command");
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

}
// declared :Commands
class factorial : Command {
    public factorial() {
        name = "factorial";
        help = "int as input, returns factorial";
        //comms = 
    }
    public override bool metodo(machine mach, 
    params string[] addComms) {
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
        Console.WriteLine();
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
                mach.memory().printReg("Show memory...");
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
// end environment if mach.environment != null
//
class exit : Command {
    public exit() {
        name = "exit";
        help = "Ends application";
        requires = false;
        comms = new Tuple[] {
            new Tuple("-m", "add comment"),
            new Tuple("-a", "Ends all environments"),
            new Tuple("-f", "Force close Environment")
        };
    }
    public override bool metodo(machine mach, 
    params string[] addComms) {
        if(!base.metodo(mach, addComms)) {
            return false;
        }
        string msg = "ended";
        environment enviro = mach.environment;
        if(addComms.Length == 0) {
            return mach.endEnvironment(enviro, msg);
        }
        for(int i = 0; i < addComms.Length; i++) {
            switch(addComms[i]) {
                case "-m":
                if(i+1 < addComms.Length) {
                    msg = addComms[i+1];
                }
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
        // TODO add any environment for adminMachine
        return mach.endEnvironment(enviro, msg);
    }
}
// rename machine or environment name
class rename : Command {
    public rename() {
        name = "rename";
        help = "rename ongoing environment";
        requires = true;
        comms = new Tuple[] {
         new Tuple("-m", "Change current machine name"),
         new Tuple("-e", "Change current environment name")   
        };
    }
    public override bool metodo(machine mach,
    params string[] addComms) {
        if(!base.metodo(mach, addComms)) {
            return false;
        }
        for(int i = 0; i < addComms.Length; i++) {
            if(addComms[i] == "-m") {
                if(i+1 < addComms.Length) {
                    mach.rename(addComms[i+1]);
                    return true;
                } else {
                return false;
                }
            } 
            if(addComms[i] == "-e") {
                if(i+1 < addComms.Length) {
                    mach.environment.rename(addComms[i+1]);
                    return true;
                } else {
                    return false;
                }
            }

        }
        return false;
    }
}
class name : Command {
    public name() {
        name = "name";
        help = "get name from Machines or Environment";
        requires = false;
        comms = new Tuple[] {
            // new Tuple("-c", "current Machine and Envi "),
            new Tuple("-m", "get all Machines on current Environment"),
            new Tuple("-e", "get all Environments")
        };
    }
    public override bool metodo(machine mach, 
    params string[] addComms) {
        if(!base.metodo(mach, addComms)) {
            return false;
        }
    if(addComms.Length == 0 || addComms[0] == "") {
            mach.respond("Current status:");
            mach.respond("Machine: \t \t" + mach.getName());
            mach.respond("Environment: \t \t" + mach.environment.getName());
            Console.WriteLine();
            return true;
        }
        for(int i = 0; i < addComms.Length; i++) {
            if(addComms[i] == "-m") {
                string temp = mach.environment.getName();
                mach.respond("Machines in" + temp); 
                
                foreach(machine mech in mach.environment.mechs) {
                    mach.respond(mech.getName());
                }
                Console.WriteLine();
                return true;
            } 
            if(addComms[i] == "-e") {
                mach.respond("All Environments");
                foreach(environment env in environment.envs) {
                    string str = " ";
                    if(mach.environment == env) {
                        str = "*";
                    }
                    mach.respond(str + env.getName());
                }
                Console.WriteLine();
                return true;
            }

        }
        return true;
    } 
}
class listc : Command {
    public listc() {
        name = "listc";
        help = "Lists all commands in current machine";
        requires = false;
        comms = new Tuple[] {};
    }
    public override bool metodo(machine mach, 
    params string[] addComms) {
        if(!base.metodo(mach, addComms)) {
            return false;
        }
        mach.commz().listComms(mach);
        Console.WriteLine();
        return true;
    }
}
class neu : Command {
    public neu() {
        name = "new";
        help = "Creates new instances of Machine or Envinronment";
        requires = true;
        comms = new Tuple[] {
            new Tuple("-e", "create new Environment"),
            new Tuple("-m", "Create new Machine")
        };
    }
    public override bool metodo(machine mach, params string[] addComms) {
        if(!base.metodo(mach, addComms)) {
            return false;
        }
        environment env = null;
        machine mech;
        foreach(string str in addComms) {
            switch(str) {
                case "-e":
                // new environment 
                env = new environment();
                break;
                case "-m":
                // if ex env mech on env, else on mach.env
                if(env != null) {
                    mech = new machine(env);
                } else {
                    mech = new machine(mach.environment); 
                }
                break;
            }
        }
        return true;
    }
}
class cd : Command {
    public cd() {
        name = "cd";
        help = "Changes current Machine or Environment";
        requires = true;
        comms = new Tuple[] {
             new Tuple("-e", "Changes current Environment"),
            new Tuple("-m", "Changes current Machine")
        };
    }
    public override bool metodo(machine mach,
    params string[] addComms) {
        if(!base.metodo(mach, addComms)) {
            return false;
        }
        if(addComms.Length > 2) {
            return false;
        }
        string fstr = addComms[0];
        string sstr = addComms[1];
        switch(fstr) {
            case "-e":
            mach.environment.trycdEnv(sstr);
            break;
            case "-m":
            //TODO
            break;
            default:
            return false;
        }
        return true;
    }
}
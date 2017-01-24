using System;
using System.Collections.Generic;

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
            switch(possibleComm) {
            case "-l":
                int fn = mach.memory().getLength() - 1;
                int st = fn - 3;
                mach.memory().printReg(st, fn);
                return true;
            case "-a":
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
                    environment.envs = new List<environment>();
                    return true;
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
            switch(addComms[i]) {
            case "-m":
                if(i+1 < addComms.Length) {
                    mach.rename(addComms[i+1]);
                    return true;
                } else {
                return false;
                }
            case "-e":
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
            switch(addComms[i]) {
                case "-m":
                string temp = mach.environment.getName();
                mach.respond("Machines in" + temp); 
                
                foreach(machine mech in mach.environment.mechs) {
                    mach.respond(mech.getName());
                }
                Console.WriteLine();
                return true;
                // break;

                case "-e":
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
                // break;
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
        for(int i = 0; i < addComms.Length; i++) {
            string str = addComms[i];
            // sstr possible name for env/mach
            string sstr = null;
            if(i+1 < addComms.Length) {
                sstr = addComms[i+1];
            }
            switch(str) {
                case "-e":
                // new environment. rename if valid sstr
                env = new environment();
                if(sstr != null) {
                    env.rename(sstr);
                }
                // this fixed it
                // automatically cd to past env after exit
                // so set after first new -e needed
                if(machine.pastEnvironment == null) {
                    machine.pastEnvironment = env;
                }
                break;
                case "-m":
                // if already -e, mech on env, else on mach.env
                if(env != null) {
                    mech = new machine(env);
                } else {
                    mech = new machine(mach.environment); 
                }
                // rename if valid
                if(sstr != null) {
                    mech.rename(sstr);
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
            mach.respond("Not valid input");
            getHelp();
            return false;
        }
        string fstr = addComms[0];
        string sstr = "";
        // Console.WriteLine(fstr);
        if(addComms.Length > 1){
            sstr = addComms[1];
        }
        switch(fstr) {
            case "-e":
            mach.environment.trycdEnv(sstr);
            break;
            case "-m":
            int num;
            if(int.TryParse(sstr, out num)) {
                mach.environment.cdMachine(num);
            } else {
                mach.environment.cdMachine(sstr);
            }
            break;
            default:
            return false;
        }
        return true;
    }
}
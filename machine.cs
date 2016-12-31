using System;
// using System.Collections.Generic;
// using System.Linq;

// interacts with user
class machine {
    string name;
    // memory and available Commands
    TupleList reg = new TupleList();
    CommList comms = new CommList();
    // public bool isAdmin = true;
    public environment environment;

    // get memory
    public TupleList memory() {
        return this.reg;
    }
    public CommList commz() {
        return this.comms;
    }

    // methods
    // reads command and executes
    public void determine() {
        respond("Give a command");
        Command currentComm;
        string[] extraComms;
        string tryComm = listen();
        if(comms.isCommand(tryComm, out currentComm, out extraComms)) {
            Console.WriteLine();
            currentComm.metodo(this, extraComms);
        } else {
            respond(tryComm + " is not a valid command");
            Console.WriteLine();
        }
    } 
    // WriteLine and saves to memory
    public void respond(string str) {
        reg.addReg("Machine", str);
        Console.WriteLine(str);
    }
    // ReadLine and saves to memory
    public string listen() {
        string str = Console.ReadLine();
        reg.addReg("User", str);
        return str;
    }
    // remember who what
    public void listen(string who, string what) {
        reg.addReg(who, what);
    }
    public int askNumber() {
        int num;
        while (true) {
            if(Int32.TryParse(listen(), out num)) {
                break;
            } else {
                Console.WriteLine("De un valor valido");
            }
        }
        return num;
    }
    public string askString() {
        string str = "";
        str = Console.ReadLine();
        return str;
    }
    public bool endEnvironment(string envire, string msg) {
        if(this.environment == null) {
            return false;
        }
        // normally listen("Environment", "Ended")
        this.listen(envire, msg);
        this.environment.pause();
        return true;
    }
    public void rename(string str) {
        name = str;
    }
    public string getName() {
        return name;
    }
    // constructor
    void _machine() {
        Console.WriteLine();
        respond("Hello World");  
        comms.addComm(new factorial());
        comms.addComm(new showMemory());
        comms.addComm(new exit());
        comms.addComm(new rename());
        comms.addComm(new name());
        comms.addComm(new listc());
        comms.addComm(new neu());
    }
    // constructor. adds Commands to CommList
    public machine() {
        _machine();
    }
    // only constructor called, name = Machine always
    public machine(environment envire) {
        _machine();
        name = "Machine";
        this.environment = envire;
        environment.mechs.Add(this);
    }
}

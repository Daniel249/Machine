using System;
// using System.Collections.Generic;
// using System.Linq;

// interacts with user
class machine {
    // memory and available Commands
    TupleList reg = new TupleList();
    CommList comms = new CommList();

    // get memory
    public TupleList memory() {
        return this.reg;
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
        }
    } 
    // constructor. adds Commands to CommList
    public machine() {
        Console.WriteLine();
        respond("Hello World");  
        comms.addComm(new factorial());
        comms.addComm(new showMemory());
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
    //function
    //ded function, remove
    // public int fact(int x) {
    //         int res = 1;
    //         for(int i = 2; i < x; i++){
    //             res = res * i;
    //         }
    //         return res;
    // }
}

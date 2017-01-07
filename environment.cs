using System;
// using System.Linq;
using System.Collections.Generic;

class environment {
    string name;
    int machNum = 0;    
    public List<machine> mechs;
    // hels name Environment{num}
    static int num = 0;
    public static List<environment> envs = new List<environment>();
    static TupleList reg = new TupleList();

    // constructor
    public environment() {
        mechs = new List<machine>();
        noch = true;
        name = "Environment" + num.ToString();
        num++;
        this.mechs.Add(machine.getAdmin());
        spawn();
    }

    // main method for environments
    // determine on mechs[machNum] `
    // normally returns nextEnvironment to cycle
    // if envs is empty return null. Ends application
    bool noch;
    public environment mein() {
        // environment pastEnvironment;
        noch = true;
        while(noch) {
            mechs[machNum].determine();
            // Console.WriteLine(machine.nextEnvironment.getName());
        }
        environment nextEnv = machine.nextEnvironment;
        // nextEnvironment goes back to this so that exit returns hier
        if(envs.Count == 0) {
            return null;
        }
        return nextEnv;
    }
    // calls main on running environment
    // after each run cycles with nextEnvironment
    // notice: if environment returned by mein is null end application
    public static void run() {
        bool noch = true;
        environment env = new environment();
        // security check nextEnvironment isnt null
        machine.nextEnvironment = env;
        while(noch) {
            // adminMachin focus on current environment 
            machine.getAdmin().environment = env;
            env = env.mein();
            if(env == null) {
                noch = false;
            }
        }
    }
    // try change environment
    public bool trycdEnv(string envName) {
        environment checkEnv = null;
        // check for coincidence, if found 
        foreach(environment env in envs) {
            if(env.getName() == envName) {
                checkEnv = env;
                break;
            }
        }
        if(checkEnv != null) {
            // set next to found env
            machine.nextEnvironment = checkEnv;
            // set past to this so exit returns hier
            machine.pastEnvironment = this;
            this.pause();
            return true;
        } else {
            return false;
        }
    }
    // change machine based on name 
    public bool cdMachine(string str) {
        for(int i = 0; i < mechs.Count; i++) {
            if(mechs[i].getName() == str) {
                machNum = i;
                return true;
            }
        }
        return false;
    }
    // change machine based on number
    public bool cdMachine(int num) {
        if(num < mechs.Count) {
            machNum = num;
            return true;
        } else {
            return false;
        }
    }
    // exit program
    // TODO only admins remember
    public void pause() {
        noch = false;
    }
    public bool remove() {
        return envs.Remove(this);
    }
    public bool weiter() {
        mein();
        return true;
    }
    // rename environment
    public void rename(string name) {
        this.name = name;
    }
    // adds new environment to envs
    void spawn() {
        envs.Add(this);
    }
    public string getName() {
        return name;
    }
}
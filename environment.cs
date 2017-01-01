using System;
// using System.Linq;
using System.Collections.Generic;

class environment {
    bool noch;
    string name;
    public List<machine> mechs;
    static int num = 0;
    public static List<environment> envs = new List<environment>();
    static TupleList reg = new TupleList();

    // constructor
    public environment() {
        mechs = new List<machine>();
        noch = true;
        name = "Environment" + num.ToString();
        num++;
        machine.nextEnvironment = this;
        this.mechs.Add(machine.getAdmin());
        spawn();
    }

    // main method for environments, at the end calls nextEnvironment.mein()
    public void mein() {
        // machine mach = new machine(this); // moved to pro.Main()
        // just test, get loop to machine
        noch = true;
        while(noch) {
            mechs[0].determine();
            // Console.WriteLine("esta es la version");
            // mach.memory().printReg(); 
        }
        machine.nextEnvironment.mein();
    }
    // try change environment
    public bool trycdEnv(string envName) {
        environment checkEnv = null;
        foreach(environment env in envs) {
            if(env.getName() == envName) {
                checkEnv = env;
                break;
            }
        }
        if(checkEnv != null) {
            this.pause();
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
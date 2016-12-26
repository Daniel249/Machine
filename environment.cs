using System;
// using System.Linq;
using System.Collections.Generic;

class environment {
    bool noch;
    string name;
    List<machine> mechs;
    int num = 0;
    static List<environment> envs = new List<environment>();
    static TupleList reg = new TupleList();

    // constructor
    public environment() {
        mechs = new List<machine>();
        noch = true;
        name = "Environment" + num.ToString();
        num++;
    }

    // main
    public void mein() {
        machine mach = new machine(this);
        // just test, get loop to machine
        noch = true;
        while(noch) {
            mach.determine();
            // Console.WriteLine("esta es la version");
            // mach.memory().printReg(); 
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

}
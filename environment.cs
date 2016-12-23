using System;

class Environment {
    bool noch = true;

    // main
    public void mein() {
        machine mach = new machine();
        // just test, get loop to machine
        while(noch) {
            mach.determine();
            Console.WriteLine("esta es la version");
            //mach.memory().printReg(); //
        }
    }
    // exit program
    // TODO only admins remember
    public void exit(machine mach, string msg) {
        mach.listen("Environment", "Terminating...");
        noch = false;
    }

}
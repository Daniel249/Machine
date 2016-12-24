using System;

class environment {
    bool noch = true;

    // main
    public void mein() {
        machine mach = new machine(this);
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
        msg = "Terminating environment";
        mach.listen("Environment", "Terminating...");
        noch = false;
    }

}
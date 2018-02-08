using System;
using System.Collections.Generic;

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


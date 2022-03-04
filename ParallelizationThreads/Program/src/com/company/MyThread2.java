package com.company;



public class MyThread2 extends Thread {

    static Singleton instance = Singleton.getInstance();

    @Override
    public void run() {
        while (instance.buffer1.size() != 0 || instance.isFirst) {

            if (instance.buffer1.size() == 0) continue;

            synchronized (instance.buffer1) {
                System.out.println("Поток 2. cos("+instance.buffer1.get(instance.buffer1.size()-1)+")=" + Math.cos(instance.buffer1.get(instance.buffer1.size()-1)));
                instance.buffer1.remove(instance.buffer1.size()-1);
                instance.buffer1.notify();
            }
        }
    }
}

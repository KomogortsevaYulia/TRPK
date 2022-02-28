package com.company;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.ThreadLocalRandom;

public class Main {

    static final int N = 3;
    //static final Object lock1 = new Object();

    static volatile Boolean isFirst = false;

    static List<Integer> buffer1 = new ArrayList<>();

    public static void main(String[] args) throws InterruptedException {
        startFirstThread();
        Thread.sleep(1000);
        startSecondThread();
    }
    public static void startFirstThread() {
        Thread thread = new MyThread1();
        thread.start();
    }

    public static void startSecondThread() {
        Thread thread = new MyThread2();
        thread.start();
    }

    public static class MyThread1 extends Thread {

        private  int counter = 0;

        @Override
        public void run() {
            isFirst = true;
            while (counter < 167 ) {
                synchronized (buffer1) {

                    ThreadLocalRandom random = ThreadLocalRandom.current();
                    buffer1.add(0,random.nextInt(184-10+1) + 10);

                    System.out.println("Buffer= " + buffer1);
                    counter++;

                    if (buffer1.size() <= N) {
                        try {
                            buffer1.wait();
                        } catch (InterruptedException e) {
                            e.printStackTrace();
                        }
                    }
                }
            }
            isFirst = false;
        }
    }

    public static class MyThread2 extends Thread {

        @Override
        public void run() {
             while (buffer1.size() != 0 || isFirst) {

                    if (buffer1.size() == 0) continue;

                    synchronized (buffer1) {
                        System.out.println("cos("+buffer1.get(buffer1.size()-1)+")=" + Math.cos(buffer1.get(buffer1.size()-1)));
                        buffer1.remove(buffer1.size()-1);
                        buffer1.notify();
                    }
            }
        }
    }
}

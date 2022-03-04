package com.company;

import java.util.ArrayList;
import java.util.List;

public class Main {
    public static void main(String[] args) throws InterruptedException {
        Thread thread = new MyThread1();    //создаем объект потока 1
        thread.start();                     //запускаем поток 1
        Thread.sleep(3000);             // пауза
        Thread thread2 = new MyThread2();   //создаем объект потока 2
        thread2.start();                    //запускаем поток 2
    }
}

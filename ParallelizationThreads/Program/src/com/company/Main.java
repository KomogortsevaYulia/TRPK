package com.company;

public class Main {
    public static void main(String[] args) throws InterruptedException {
        Thread thread = new Thread1();    //создаем объект потока 1
        thread.start();                     //запускаем поток 1
        //Thread.sleep(3000);             //приостанавливаем поток
        Thread thread2 = new Thread2();   //создаем объект потока 2
        thread2.start();                    //запускаем поток 2
    }

}

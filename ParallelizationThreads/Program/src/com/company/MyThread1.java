package com.company;

import java.util.concurrent.ThreadLocalRandom;



public class MyThread1 extends Thread {

    static Singleton instance = Singleton.getInstance();

    private  int counter = 0;//счетчик на кол-во сгенерируемых чисел

    @Override
    public void run() {     //переопределяем метод Run
        instance.isFirst = true;
        while (counter < 167 ) {    //цикл на генерацию чисел
            synchronized (instance.buffer1) {

                ThreadLocalRandom random = ThreadLocalRandom.current(); //обьект класса предназначенный для генерации рандомных чисел в потоках
                instance.buffer1.add(0,random.nextInt(184-10+1) + 10);//добавляем в буфер сгенерированное число в границах от 10 до 184

                System.out.println("Поток 1. буфер= " + instance.buffer1);
                counter++; //увеличиваем счетчик

                if (instance.buffer1.size() <= instance.N) { //условие на полностью заполненный буфер
                    try {
                        instance.buffer1.wait();     //если сможем положить
                    } catch (InterruptedException e) {
                        e.printStackTrace();
                    }
                }
            }
        }
        instance.isFirst = false;
    }
}
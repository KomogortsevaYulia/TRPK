package com.company;

import java.util.concurrent.ThreadLocalRandom;

public class Thread1 extends Thread {

    static Singleton instance = Singleton.getInstance();

    private int counter = 0;//счетчик на кол-во сгенерируемых чисел

    @Override
    public void run() {     //переопределяем метод Run
        instance.isFirst=true;
        while (counter < 167) {    //цикл на генерацию чисел
            if (instance.buffer1.size() >= instance.N) { //условие на заполненный буфер
                try {
                    instance.buffer1.wait();     //освобождает монитор и переводит поток1 в состояние ожидания до тех пор, пока поток2 не вызовет метод notify()
                } catch (Exception e) {

                }
            } else {
                synchronized (instance.buffer1) { //будет блокировать доступ к коду, если буфер уже использует другой поток
                    ThreadLocalRandom random = ThreadLocalRandom.current(); //обьект класса предназначенный для генерации рандомных чисел в потоках
                    instance.buffer1.add(0, random.nextInt(184 - 10 + 1) + 10);//добавляем в буфер сгенерированное число в границах от 10 до 184

                    System.out.println("Поток 1. буфер= " + instance.buffer1);
                    counter++; //увеличиваем счетчик
                }
            }
        }
        instance.isFirst=false;
    }

}
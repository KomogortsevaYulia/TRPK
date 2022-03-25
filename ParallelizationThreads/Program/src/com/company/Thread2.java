package com.company;
public class Thread2 extends Thread {

    static Singleton instance = Singleton.getInstance();

    @Override
    public void run() {
        while (instance.buffer1.size() != 0 || instance.isFirst) { //пока буфер не пустой или первый поток не закончил выполнение

            //если попали в цикл т к первый поток не завершил работу
            // может оказаться что буфер пустой,значит не можем отсюда взять цифры,значит надо пропустить код после условия и вернуться в начало цикла
            if (instance.buffer1.size() == 0) continue;


            synchronized (instance.buffer1) { //будет блокировать доступ к коду, если буфер уже использует другой поток
                System.out.println("Поток 2. cos("+instance.buffer1.get(instance.buffer1.size()-1)+")=" + Math.cos(instance.buffer1.get(instance.buffer1.size()-1)));//выводим результат
                instance.buffer1.remove(instance.buffer1.size()-1); //удаляем из буфера
                instance.buffer1.notify(); //возобновим выполнение потока, из которого был вызван метод wait() для буфера
            }
        }
    }
}

package com.company;

import java.util.ArrayList;
import java.util.List;

public final class Singleton {

    final int N = 3;    //размер буфера

    volatile Boolean isFirst = false;

    volatile List<Integer> buffer1 = new ArrayList<>(); // сам буфер

    private static volatile Singleton instance;

    public static Singleton getInstance() {
        Singleton result = instance;
        if (result != null) {
            return result;
        }
        synchronized (Singleton.class) {
            if (instance == null) {
                instance = new Singleton();
            }
            return instance;
        }
    }
}

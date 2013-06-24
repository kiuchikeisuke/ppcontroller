package jp.ne.nissing.powerpointcontroller.manager;

import java.util.HashMap;

import android.graphics.Bitmap;

public class ImageCache {
	private static HashMap<Integer, Bitmap> cache = new HashMap<Integer, Bitmap>();

	public static void setCache(int key, Bitmap value){
		cache.put(key, value);
	}
	
	public static Bitmap getCache(int key){
		if(cache.containsKey(key))
			return cache.get(key);
		return null;
	}
	
	public static void clearCache(){
		cache.clear();
		cache = null;
		cache = new HashMap<Integer, Bitmap>();
	}
}

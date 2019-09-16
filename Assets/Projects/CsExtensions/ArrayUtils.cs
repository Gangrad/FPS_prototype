namespace CsExtensions {
    public static class ArrayUtils {
        public static void Fill<T>(this T[] array, T obj) {
            for (var i = 0; i < array.Length; ++i)
                array[i] = obj;
        }
        
        
    }
}
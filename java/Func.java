
public class Func {
    public Func() {
    }

    public static <T> T requireNotNull(T obj) {
        return Objects.requireNonNull(obj);
    }

    public static <T> T requireNotNull(T obj, String message) {
        return Objects.requireNonNull(obj, message);
    }

    public static <T> T requireNotNull(T obj, Supplier<String> messageSupplier) {
        return Objects.requireNonNull(obj, messageSupplier);
    }

    public static boolean isNull(@Nullable Object obj) {
        return Objects.isNull(obj);
    }

    public static boolean notNull(@Nullable Object obj) {
        return Objects.nonNull(obj);
    }

    public static String firstCharToLower(String str) {
        return StringUtil.firstCharToLower(str);
    }

    public static String firstCharToUpper(String str) {
        return StringUtil.firstCharToUpper(str);
    }

    public static boolean isBlank(@Nullable final CharSequence cs) {
        return StringUtil.isBlank(cs);
    }

    public static boolean isNotBlank(@Nullable final CharSequence cs) {
        return StringUtil.isNotBlank(cs);
    }

    public static boolean isAnyBlank(final CharSequence... css) {
        return StringUtil.isAnyBlank(css);
    }

    public static boolean isNoneBlank(final CharSequence... css) {
        return StringUtil.isNoneBlank(css);
    }

    public static boolean isArray(@Nullable Object obj) {
        return ObjectUtil.isArray(obj);
    }

    public static boolean isEmpty(@Nullable Object obj) {
        return ObjectUtil.isEmpty(obj);
    }

    public static boolean isNotEmpty(@Nullable Object obj) {
        return !ObjectUtil.isEmpty(obj);
    }

    public static boolean isEmpty(@Nullable Object[] array) {
        return ObjectUtil.isEmpty(array);
    }

    public static boolean isNotEmpty(@Nullable Object[] array) {
        return ObjectUtil.isNotEmpty(array);
    }

    public static boolean hasEmpty(Object... os) {
        Object[] var1 = os;
        int var2 = os.length;

        for(int var3 = 0; var3 < var2; ++var3) {
            Object o = var1[var3];
            if (isEmpty(o)) {
                return true;
            }
        }

        return false;
    }

    public static boolean isAllEmpty(Object... os) {
        Object[] var1 = os;
        int var2 = os.length;

        for(int var3 = 0; var3 < var2; ++var3) {
            Object o = var1[var3];
            if (isNotEmpty(o)) {
                return false;
            }
        }

        return true;
    }

    public static String format(@Nullable String message, @Nullable Map<String, ?> params) {
        return StringUtil.format(message, params);
    }

    public static String format(@Nullable String message, @Nullable Object... arguments) {
        return StringUtil.format(message, arguments);
    }

    public static String format(long nanos) {
        return StringUtil.format(nanos);
    }

    public static boolean equals(Object obj1, Object obj2) {
        return Objects.equals(obj1, obj2);
    }

    public static boolean equalsSafe(@Nullable Object o1, @Nullable Object o2) {
        return ObjectUtil.nullSafeEquals(o1, o2);
    }

    public static <T> boolean contains(@Nullable T[] array, final T element) {
        return CollectionUtil.contains(array, element);
    }

    public static boolean contains(@Nullable Iterator<?> iterator, Object element) {
        return CollectionUtil.contains(iterator, element);
    }

    public static boolean contains(@Nullable Enumeration<?> enumeration, Object element) {
        return CollectionUtil.contains(enumeration, element);
    }

    @SafeVarargs
    public static <E> Set<E> ofImmutableSet(E... es) {
        return CollectionUtil.ofImmutableSet(es);
    }

    @SafeVarargs
    public static <E> List<E> ofImmutableList(E... es) {
        return CollectionUtil.ofImmutableList(es);
    }

    public static String toStr(Object str) {
        return toStr(str, "");
    }

    public static String toStr(Object str, String defaultValue) {
        return null != str && !str.equals("null") ? String.valueOf(str) : defaultValue;
    }

    public static String toStrWithEmpty(Object str, String defaultValue) {
        return null != str && !str.equals("null") && !str.equals("") ? String.valueOf(str) : defaultValue;
    }

    public static boolean isNumeric(final CharSequence cs) {
        return StringUtil.isNumeric(cs);
    }

    public static int toInt(final Object str) {
        return NumberUtil.toInt(String.valueOf(str));
    }

    public static int toInt(@Nullable final Object str, final int defaultValue) {
        return NumberUtil.toInt(String.valueOf(str), defaultValue);
    }

    public static long toLong(final Object str) {
        return NumberUtil.toLong(String.valueOf(str));
    }

    public static long toLong(@Nullable final Object str, final long defaultValue) {
        return NumberUtil.toLong(String.valueOf(str), defaultValue);
    }

    public static Double toDouble(Object value) {
        return toDouble(String.valueOf(value), -1.0);
    }

    public static Double toDouble(Object value, Double defaultValue) {
        return NumberUtil.toDouble(String.valueOf(value), defaultValue);
    }

    public static Float toFloat(Object value) {
        return toFloat(String.valueOf(value), -1.0F);
    }

    public static Float toFloat(Object value, Float defaultValue) {
        return NumberUtil.toFloat(String.valueOf(value), defaultValue);
    }

    public static Boolean toBoolean(Object value) {
        return toBoolean(value, (Boolean)null);
    }

    public static Boolean toBoolean(Object value, Boolean defaultValue) {
        if (value != null) {
            String val = String.valueOf(value);
            val = val.toLowerCase().trim();
            return Boolean.parseBoolean(val);
        } else {
            return defaultValue;
        }
    }

    public static Integer[] toIntArray(String str) {
        return toIntArray(",", str);
    }

    public static Integer[] toIntArray(String split, String str) {
        if (StringUtil.isEmpty(str)) {
            return new Integer[0];
        } else {
            String[] arr = str.split(split);
            Integer[] ints = new Integer[arr.length];

            for(int i = 0; i < arr.length; ++i) {
                Integer v = toInt(arr[i], 0);
                ints[i] = v;
            }

            return ints;
        }
    }

    public static List<Integer> toIntList(String str) {
        return Arrays.asList(toIntArray(str));
    }

    public static List<Integer> toIntList(String split, String str) {
        return Arrays.asList(toIntArray(split, str));
    }

    public static Integer firstInt(String str) {
        return firstInt(",", str);
    }

    public static Integer firstInt(String split, String str) {
        List<Integer> ints = toIntList(split, str);
        return isEmpty((Object)ints) ? null : (Integer)ints.get(0);
    }

    public static Long[] toLongArray(String str) {
        return toLongArray(",", str);
    }

    public static Long[] toLongArray(String split, String str) {
        if (StringUtil.isEmpty(str)) {
            return new Long[0];
        } else {
            String[] arr = str.split(split);
            Long[] longs = new Long[arr.length];

            for(int i = 0; i < arr.length; ++i) {
                Long v = toLong(arr[i], 0L);
                longs[i] = v;
            }

            return longs;
        }
    }

    public static List<Long> toLongList(String str) {
        return Arrays.asList(toLongArray(str));
    }

    public static List<Long> toLongList(String split, String str) {
        return Arrays.asList(toLongArray(split, str));
    }

    public static Long firstLong(String str) {
        return firstLong(",", str);
    }

    public static Long firstLong(String split, String str) {
        List<Long> longs = toLongList(split, str);
        return isEmpty((Object)longs) ? null : (Long)longs.get(0);
    }

    public static String[] toStrArray(String str) {
        return toStrArray(",", str);
    }

    public static String[] toStrArray(String split, String str) {
        return isBlank(str) ? new String[0] : str.split(split);
    }

    public static List<String> toStrList(String str) {
        return Arrays.asList(toStrArray(str));
    }

    public static List<String> toStrList(String split, String str) {
        return Arrays.asList(toStrArray(split, str));
    }

    public static String firstStr(String str) {
        return firstStr(",", str);
    }

    public static String firstStr(String split, String str) {
        List<String> strs = toStrList(split, str);
        return isEmpty((Object)strs) ? null : (String)strs.get(0);
    }

    public static String to62String(long num) {
        return NumberUtil.to62String(num);
    }

    public static String join(Collection<?> coll) {
        return StringUtil.join(coll);
    }

    public static String join(Collection<?> coll, String delim) {
        return StringUtil.join(coll, delim);
    }

    public static String join(Object[] arr) {
        return StringUtil.join(arr);
    }

    public static String join(Object[] arr, String delim) {
        return StringUtil.join(arr, delim);
    }

    public static List<String> split(CharSequence str, char separator) {
        return StringUtil.split(str, separator, -1);
    }

    public static List<String> splitTrim(CharSequence str, char separator) {
        return StringUtil.splitTrim(str, separator);
    }

    public static List<String> splitTrim(CharSequence str, CharSequence separator) {
        return StringUtil.splitTrim(str, separator);
    }

    public static String[] split(@Nullable String str, @Nullable String delimiter) {
        return StringUtil.delimitedListToStringArray(str, delimiter);
    }

    public static String[] splitTrim(@Nullable String str, @Nullable String delimiter) {
        return StringUtil.delimitedListToStringArray(str, delimiter, " \t\n\n\f");
    }

    public static boolean simpleMatch(@Nullable String pattern, @Nullable String str) {
        return PatternMatchUtils.simpleMatch(pattern, str);
    }

    public static boolean simpleMatch(@Nullable String[] patterns, String str) {
        return PatternMatchUtils.simpleMatch(patterns, str);
    }

    public static String randomUUID() {
        return StringUtil.randomUUID();
    }

    public static String escapeHtml(String html) {
        return StringUtil.escapeHtml(html);
    }

    public static String random(int count) {
        return StringUtil.random(count);
    }

    public static String random(int count, RandomType randomType) {
        return StringUtil.random(count, randomType);
    }

    public static String md5Hex(final String data) {
        return DigestUtil.md5Hex(data);
    }

    public static String md5Hex(final byte[] bytes) {
        return DigestUtil.md5Hex(bytes);
    }

    public static String sha1Hex(String data) {
        return DigestUtil.sha1Hex(data);
    }

    public static String sha1Hex(final byte[] bytes) {
        return DigestUtil.sha1Hex(bytes);
    }

    public static String sha224Hex(String data) {
        return DigestUtil.sha224Hex(data);
    }

    public static String sha224Hex(final byte[] bytes) {
        return DigestUtil.sha224Hex(bytes);
    }

    public static String sha256Hex(String data) {
        return DigestUtil.sha256Hex(data);
    }

    public static String sha256Hex(final byte[] bytes) {
        return DigestUtil.sha256Hex(bytes);
    }

    public static String sha384Hex(String data) {
        return DigestUtil.sha384Hex(data);
    }

    public static String sha384Hex(final byte[] bytes) {
        return DigestUtil.sha384Hex(bytes);
    }

    public static String sha512Hex(String data) {
        return DigestUtil.sha512Hex(data);
    }

    public static String sha512Hex(final byte[] bytes) {
        return DigestUtil.sha512Hex(bytes);
    }

    public static String hmacMd5Hex(String data, String key) {
        return DigestUtil.hmacMd5Hex(data, key);
    }

    public static String hmacMd5Hex(final byte[] bytes, String key) {
        return DigestUtil.hmacMd5Hex(bytes, key);
    }

    public static String hmacSha1Hex(String data, String key) {
        return DigestUtil.hmacSha1Hex(data, key);
    }

    public static String hmacSha1Hex(final byte[] bytes, String key) {
        return DigestUtil.hmacSha1Hex(bytes, key);
    }

    public static String hmacSha224Hex(String data, String key) {
        return DigestUtil.hmacSha224Hex(data, key);
    }

    public static String hmacSha224Hex(final byte[] bytes, String key) {
        return DigestUtil.hmacSha224Hex(bytes, key);
    }

    public static String hmacSha256Hex(String data, String key) {
        return DigestUtil.hmacSha256Hex(data, key);
    }

    public static String hmacSha256Hex(final byte[] bytes, String key) {
        return DigestUtil.hmacSha256Hex(bytes, key);
    }

    public static String hmacSha384Hex(String data, String key) {
        return DigestUtil.hmacSha384Hex(data, key);
    }

    public static String hmacSha384Hex(final byte[] bytes, String key) {
        return DigestUtil.hmacSha384Hex(bytes, key);
    }

    public static String hmacSha512Hex(String data, String key) {
        return DigestUtil.hmacSha512Hex(data, key);
    }

    public static String hmacSha512Hex(final byte[] bytes, String key) {
        return DigestUtil.hmacSha512Hex(bytes, key);
    }

    public static String encodeHex(byte[] bytes) {
        return DigestUtil.encodeHex(bytes);
    }

    public static byte[] decodeHex(final String hexString) {
        return DigestUtil.decodeHex(hexString);
    }

    public static String encodeBase64(String value) {
        return Base64Util.encode(value);
    }

    public static String encodeBase64(String value, Charset charset) {
        return Base64Util.encode(value, charset);
    }

    public static String encodeBase64UrlSafe(String value) {
        return Base64Util.encodeUrlSafe(value);
    }

    public static String encodeBase64UrlSafe(String value, Charset charset) {
        return Base64Util.encodeUrlSafe(value, charset);
    }

    public static String decodeBase64(String value) {
        return Base64Util.decode(value);
    }

    public static String decodeBase64(String value, Charset charset) {
        return Base64Util.decode(value, charset);
    }

    public static String decodeBase64UrlSafe(String value) {
        return Base64Util.decodeUrlSafe(value);
    }

    public static String decodeBase64UrlSafe(String value, Charset charset) {
        return Base64Util.decodeUrlSafe(value, charset);
    }

    public static void closeQuietly(@Nullable Closeable closeable) {
        IoUtil.closeQuietly(closeable);
    }

    public static String readToString(InputStream input) {
        return IoUtil.readToString(input);
    }

    public static String readToString(@Nullable InputStream input, Charset charset) {
        return IoUtil.readToString(input, charset);
    }

    public static byte[] readToByteArray(@Nullable InputStream input) {
        return IoUtil.readToByteArray(input);
    }

    public static String readToString(final File file) {
        return FileUtil.readToString(file);
    }

    public static String readToString(File file, Charset encoding) {
        return FileUtil.readToString(file, encoding);
    }

    public static byte[] readToByteArray(File file) {
        return FileUtil.readToByteArray(file);
    }

    public static String toJson(Object object) {
        return JsonUtil.toJson(object);
    }

    public static byte[] toJsonAsBytes(Object object) {
        return JsonUtil.toJsonAsBytes(object);
    }

    public static JsonNode readTree(String jsonString) {
        return JsonUtil.readTree(jsonString);
    }

    public static JsonNode readTree(InputStream in) {
        return JsonUtil.readTree(in);
    }

    public static JsonNode readTree(byte[] content) {
        return JsonUtil.readTree(content);
    }

    public static JsonNode readTree(JsonParser jsonParser) {
        return JsonUtil.readTree(jsonParser);
    }

    public static <T> T readJson(byte[] bytes, Class<T> valueType) {
        return JsonUtil.parse(bytes, valueType);
    }

    public static <T> T readJson(String jsonString, Class<T> valueType) {
        return JsonUtil.parse(jsonString, valueType);
    }

    public static <T> T readJson(InputStream in, Class<T> valueType) {
        return JsonUtil.parse(in, valueType);
    }

    public static <T> T readJson(byte[] bytes, TypeReference<T> typeReference) {
        return JsonUtil.parse(bytes, typeReference);
    }

    public static <T> T readJson(String jsonString, TypeReference<T> typeReference) {
        return JsonUtil.parse(jsonString, typeReference);
    }

    public static <T> T readJson(InputStream in, TypeReference<T> typeReference) {
        return JsonUtil.parse(in, typeReference);
    }

    public static String urlEncode(String source) {
        return UrlUtil.encode(source, Charsets.UTF_8);
    }

    public static String urlEncode(String source, Charset charset) {
        return UrlUtil.encode(source, charset);
    }

    public static String urlDecode(String source) {
        return StringUtils.uriDecode(source, Charsets.UTF_8);
    }

    public static String urlDecode(String source, Charset charset) {
        return StringUtils.uriDecode(source, charset);
    }

    public static String formatDateTime(Date date) {
        return DateUtil.formatDateTime(date);
    }

    public static String formatDate(Date date) {
        return DateUtil.formatDate(date);
    }

    public static String formatTime(Date date) {
        return DateUtil.formatTime(date);
    }

    public static String format(Object object, String pattern) {
        if (object instanceof Number) {
            DecimalFormat decimalFormat = new DecimalFormat(pattern);
            return decimalFormat.format(object);
        } else if (object instanceof Date) {
            return DateUtil.format((Date)object, pattern);
        } else if (object instanceof TemporalAccessor) {
            return DateTimeUtil.format((TemporalAccessor)object, pattern);
        } else {
            throw new IllegalArgumentException("未支持的对象:" + object + ",格式:" + object);
        }
    }

    public static Date parseDate(String dateStr, String pattern) {
        return DateUtil.parse(dateStr, pattern);
    }

    public static Date parse(String dateStr, ConcurrentDateFormat format) {
        return DateUtil.parse(dateStr, format);
    }

    public static String formatDateTime(TemporalAccessor temporal) {
        return DateTimeUtil.formatDateTime(temporal);
    }

    public static String formatDate(TemporalAccessor temporal) {
        return DateTimeUtil.formatDate(temporal);
    }

    public static String formatTime(TemporalAccessor temporal) {
        return DateTimeUtil.formatTime(temporal);
    }

    public static LocalDateTime parseDateTime(String dateStr, DateTimeFormatter formatter) {
        return DateTimeUtil.parseDateTime(dateStr, formatter);
    }

    public static LocalDateTime parseDateTime(String dateStr) {
        return DateTimeUtil.parseDateTime(dateStr);
    }

    public static LocalDate parseDate(String dateStr, DateTimeFormatter formatter) {
        return DateTimeUtil.parseDate(dateStr, formatter);
    }

    public static LocalDate parseDate(String dateStr) {
        return DateTimeUtil.parseDate(dateStr, DateTimeUtil.DATE_FORMAT);
    }

    public static LocalTime parseTime(String dateStr, DateTimeFormatter formatter) {
        return DateTimeUtil.parseTime(dateStr, formatter);
    }

    public static LocalTime parseTime(String dateStr) {
        return DateTimeUtil.parseTime(dateStr);
    }

    public static Duration between(Temporal startInclusive, Temporal endExclusive) {
        return Duration.between(startInclusive, endExclusive);
    }

    public static Duration between(Date startDate, Date endDate) {
        return DateUtil.between(startDate, endDate);
    }

    @Nullable
    public static <T> T convert(@Nullable Object source, Class<T> targetType) {
        return ConvertUtil.convert(source, targetType);
    }

    @Nullable
    public static <T> T convert(@Nullable Object source, TypeDescriptor sourceType, TypeDescriptor targetType) {
        return ConvertUtil.convert(source, sourceType, targetType);
    }

    @Nullable
    public static <T> T convert(@Nullable Object source, TypeDescriptor targetType) {
        return ConvertUtil.convert(source, targetType);
    }

    public static MethodParameter getMethodParameter(Constructor<?> constructor, int parameterIndex) {
        return ClassUtil.getMethodParameter(constructor, parameterIndex);
    }

    public static MethodParameter getMethodParameter(Method method, int parameterIndex) {
        return ClassUtil.getMethodParameter(method, parameterIndex);
    }

    @Nullable
    public static <A extends Annotation> A getAnnotation(AnnotatedElement annotatedElement, Class<A> annotationType) {
        return AnnotatedElementUtils.findMergedAnnotation(annotatedElement, annotationType);
    }

    @Nullable
    public static <A extends Annotation> A getAnnotation(Method method, Class<A> annotationType) {
        return ClassUtil.getAnnotation(method, annotationType);
    }

    @Nullable
    public static <A extends Annotation> A getAnnotation(HandlerMethod handlerMethod, Class<A> annotationType) {
        return ClassUtil.getAnnotation(handlerMethod, annotationType);
    }

    public static <T> T newInstance(Class<?> clazz) {
        return BeanUtil.instantiateClass(clazz);
    }

    public static <T> T newInstance(String clazzStr) {
        return BeanUtil.newInstance(clazzStr);
    }

    @Nullable
    public static Object getProperty(@Nullable Object bean, String propertyName) {
        return BeanUtil.getProperty(bean, propertyName);
    }

    public static void setProperty(Object bean, String propertyName, Object value) {
        BeanUtil.setProperty(bean, propertyName, value);
    }

    @Nullable
    public static <T> T clone(@Nullable T source) {
        return BeanUtil.clone(source);
    }

    @Nullable
    public static <T> T copy(@Nullable Object source, Class<T> clazz) {
        return BeanUtil.copy(source, clazz);
    }

    public static void copy(@Nullable Object source, @Nullable Object targetBean) {
        BeanUtil.copy(source, targetBean);
    }

    public static void copyNonNull(@Nullable Object source, @Nullable Object targetBean) {
        BeanUtil.copyNonNull(source, targetBean);
    }

    @Nullable
    public static <T> T copyWithConvert(@Nullable Object source, Class<T> clazz) {
        return BeanUtil.copyWithConvert(source, clazz);
    }

    public static <T> List<T> copy(@Nullable Collection<?> sourceList, Class<T> targetClazz) {
        return BeanUtil.copy(sourceList, targetClazz);
    }

    public static <T> List<T> copyWithConvert(@Nullable Collection<?> sourceList, Class<T> targetClazz) {
        return BeanUtil.copyWithConvert(sourceList, targetClazz);
    }

    @Nullable
    public static <T> T copyProperties(@Nullable Object source, Class<T> clazz) throws BeansException {
        return BeanUtil.copyProperties(source, clazz);
    }

    public static <T> List<T> copyProperties(@Nullable Collection<?> sourceList, Class<T> targetClazz) throws BeansException {
        return BeanUtil.copyProperties(sourceList, targetClazz);
    }

    public static Map<String, Object> toMap(@Nullable Object bean) {
        return BeanUtil.toMap(bean);
    }

    public static <T> T toBean(Map<String, Object> beanMap, Class<T> valueType) {
        return BeanUtil.toBean(beanMap, valueType);
    }
}
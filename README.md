# csv-compare-with-key

Compare tow csv file with defined keys

# Useage

csv-compare-with-key -i <index csv file>  -k <index csv keys> -c <compare csv file> -e <compare csv keys> [options]

+  -i, --index-csv        Required. Input csv file to be indexed.
+  -k, --index-keys       Required. Input csv file to be indexed key columns.
+  -c, --compare-csv      Required. Input csv file to be compared.
+  -e, --compare-keys     Required. Input csv file to be compared.
+  -t, --trimes           (Default: ["]) Trimed column chars.
+  -u, --is-utf8          (Default: True) Using UTF-8 encoding.
+  -s, --split            (Default: [",","\t"]) Csv split.
+  -n, --unix-new-line    (Default: True) Using unix new line.
+  --help                 Display this help screen.

from pathlib import Path

DB = 20
siemens_file = Path(f"DB{DB}.scl")

text_header = f"""DATA_BLOCK "Data_block_{DB}"
{{ S7_Optimized_Access := 'FALSE' }}
VERSION : 0.1
NON_RETAIN
   VAR
"""

text_tail = """   END_VAR


BEGIN

END_DATA_BLOCK
"""

with open(siemens_file, "w") as f_out:
    f_out.write(text_header)
    for x in range(400):
        f_out.write(f"      S7Tag{x} : Word;\n")
    f_out.write(text_tail)

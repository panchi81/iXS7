from pathlib import Path

writefile_path = Path("TagToDB.scl")

part1 = """{ S7_Optimized_Access := 'FALSE' }
VERSION : 0.1
NON_RETAIN
   VAR\n"""
part2 = """   END_VAR


BEGIN

END_DATA_BLOCK

"""


with open(writefile_path, "w") as f_out:
    f_out.write('DATA_BLOCK "Data_block_12"\n')
    f_out.write(part1)
    for i, j in enumerate(range(0, 4000, 4)):
        f_out.write(f"      value{i} : Real;\n")
    f_out.write(part2)

#! iXS7env\Scripts\python.exe
import csv
from os import read
from pathlib import Path
from string import digits

# readfile_path = Path("C:/Users/FRAGO/Documents/Support2021/178095 - S7ISOTCP_recipe items missing/Export/export.txt")
# readfile_path = Path("export.txt")
# readfile_path = Path("removed_tags.txt")
readfile_path = Path("test_export.txt")
writefile_path = Path("Data_Block_export.scl")
delimiter = ","

data_types = {
    "DEFAULT": [0, ""],
    "BIT": [0, "Bool"],
    "BOOL": [0, "Bool"],
    "INT16": [0, "Word"],
    "UINT16": [0, "UInt"],
    "INT32": [0, "DWord"],
    "UINT32": [0, "UDInt"],
    "FLOAT": [0, "Real"],
    "DOUBLE": [0, "LReal"],
    "STRING": [0, "String"],
}

db = []
# DEBUG
# print("Testing")

with open(readfile_path, "r") as ix_tag_export, open(writefile_path, "a") as s7_db_scl:
    # initiate cleaning of header
    h_reader = csv.reader(ix_tag_export)
    # skip header
    header = next(h_reader)
    # clean up header
    header[0] = header[0].removeprefix("// ")
    header[-1] = header[-1].removesuffix(" //")

    # Work with the following reader
    reader = csv.DictReader(ix_tag_export, fieldnames=header, delimiter=delimiter)
    # skip the header
    next(reader)

    # Determine the Siemens Address Column
    for row in reader:
        for key in row:
            if row[key].startswith("DB"):
                s7_address = key
                break

        # create list of included DataBlocks
        data_block = row[s7_address].split(".")[0]
        if data_block.startswith("DB"):
            db.append(data_block)

    # prepare DataBlock dict
    # Sorted by DB
    # db_dict = {i: [] for i in sorted(set(db))}
    # unsorted dictionary
    db_dict = {i: [] for i in set(db)}

    # read from the top
    ix_tag_export.seek(0)
    # skip header
    next(reader)

    # populate DataBlock (keys) dictionary with list of Siemens S7 register addresses (values)
    for row in reader:
        db_dict.setdefault(row[s7_address].split(".")[0], []).append(row[s7_address])

    # sort primarily by DB (key), and secondarily by register address
    sorted_tags = {
        i: sorted(db_dict[i], key=lambda x: int(x.split(".")[1][3:])) for i in db_dict
    }
    # Debug
    print(f"{db_dict= }")
    print(f"{sorted_tags= }")

# ToDo: Filter out addresses within other addresses (Bit in word/float/long, word in float/long, or float in long)
# ToDo: Fill-in missing DB entries with expected (missing) datatype and enumerated Tag_name.
# ToDo: Write output file

# with open(readfile_path, "r") as csv_file:
#     reader = csv.reader(csv_file, delimiter=",")
#     for row in reader:
#         # data_types[row[1]][0]+=1
#         if row[2].split(".")[0][0:2] == "DB":
#             db.append(row[2].split(".")[0])

# unique_db = set(db)
# db_dict = {i: [] for i in unique_db}

# # total = sum(data_types.values())
# # for k,v in data_types.items():
# #     print(f"{k}\t{v}")
# # print(f"{total= }")

# # for x in sorted(unique_db):
# #     print(x)

# with open(readfile_path, "r") as second_pass:
#     reader = csv.reader(second_pass, delimiter=",")
#     for row in reader:
#         # # print(row[2])
#         db_dict.setdefault(row[2].split(".")[0], []).append(row[2])

# sorted_tags = {
#     i: sorted(db_dict[i], key=lambda x: int(x.split(".")[1][3:])) for i in db_dict
# }
# # print(sorted_tags)

# s7_datatypes = {"DBD": "Real", "DBW": "Word", "DBX": "Bool"}

# part1 = """{ S7_Optimized_Access := 'FALSE' }
# VERSION : 0.1
# NON_RETAIN
#    VAR\n"""
# part2 = """   END_VAR


# BEGIN

# END_DATA_BLOCK

# """


# with open(writefile_path, "a") as f_out:
#     for key, values in sorted_tags.items():
#         f_out.write(f'DATA_BLOCK "{key}"\n')
#         f_out.write(part1)
#         for val in values:

#             f_out.write(
#                 f"      {val.replace('.', '_')} : {s7_datatypes[val.split('.')[1].rstrip(digits)]};\n"
#             )
#         f_out.write(part2)

# # Report
# print(f"Constructed {s7_db_scl} containing:")
# print(f"{number_of_dbs} DataBlocks")
# print(f"{number_of_entries} DataBlock entries (Tags)")

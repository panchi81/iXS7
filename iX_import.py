#! iXS7env\Scripts\python.exe

from pathlib import Path


def main():

    # dir_path = Path(r"C:\\Users\\FRAGO\\Documents\\py\\Projects\\iXS7\\test\\")
    file_path = Path(
        r"C:\\Users\\FRAGO\\Documents\\py\\Projects\\iXS7\\test\\" + "iX_import.txt"
    )

    try:

        with open(file_path, "a") as f_out:
            f_out.write("// Name,DataType, Address_2 //")
            for j in range(0, 13, 4):
                f_out.write(f"\nRCP_VD_{j},INT32,DB10.DBD{j}")
            for j in range(16, 23, 2):
                f_out.write(f"\nRCP_VW_{j},INT16,DB10.DBW{j}")
            for i in range(24, 30):
                f_out.write(f"\nRCP_VB_{i},INT16,DB10.DBB{i}")
            for h in [30, 31]:
                for j in range(8):
                    f_out.write(f"\nRCP_V_{h}_{j},BIT,DB10.DBX{h}.{j}")

    except FileExistsError as bla:
        print(bla)


if __name__ == "__main__":
    main()

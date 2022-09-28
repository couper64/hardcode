# Getting Started
To convert a *.py file to *.exe file using PyInstaller, it is required to install PyInstaller package first.
Additionally, I created a virtual environment beforehand using built-in command:
```
python -m venv .venv
```
To activate installed virtual environment, I used PowerShell and the following commands:
```
Set-ExecutionPolicy Unrestricted -Scope CurrentUser
.\.venv\Scripts\Activate.ps1
Set-ExecutionPolicy Restricted -Scope CurrentUser
```
Command prompt, now, should have <span style="color:green">(.venv)</span> prefix.
To list installed PyPI packages I used the following command:
```
pip list
```
To upgrade the PIP tool, I used the following command on Windows PowerShell:
```
python.exe -m pip install --upgrade pip
```
To install required package, I run the following command:
```
pip install pyinstaller
```
To convert Python script to Windows executable, I run the following command:
```
pyinstaller.exe --onefile --noconsole --exclude-module _bootlocale .\helloworld.py
```
The ```--exclude-module _bootlocale``` parameter is optional and is related to Python version 3.10 bug as mentioned in the link:
https://stackoverflow.com/questions/68459087/pyinstaller-with-python-3-10-0b4-importerror-no-module-named-bootlocale

The other approach is to install Python 3.10 PyInstaller support files from their GitHub repository:
```
pip install https://github.com/rokm/pyinstaller/archive/refs/heads/python-3.10.zip
```
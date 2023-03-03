Some sources on the internet are using __pip install --editable .__ command.
This command installs the contents of the folder as a Python module.
Consequently, it becomes avaialble throughout the Python environment.

There are multiple approaches, I focused on __setuptools__ and __setup.py__
configuration as it seems the de-facto standard in the community. However,
a new configuration of __setuptools__ and __pyproject.toml__ is gaining
popularity, but I decided to stick with the old approach for now.

    conda create -yn setuptools-editable-setuppy python=3.9.13 setuptools=67.4.0
    conda activate setuptools-editable-setuppy

For this example, I focused on a barebones version of __setup.py__ file
without bells and whistles, but tweaking it to my liking.

    pip install --editable .

One of the reasons I decided to use __setuptools__ was because of the nested
folder structure I tend to create in order to organise my scripts. However,
Python struggles importing them when you are importing a script from another
folder.

I created a dummy folder structure and added TQDM package as a dependency.

    pip install tqdm
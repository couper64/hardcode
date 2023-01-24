def run_commands(commands : list, separate_terminal : bool = False):
    """ Run a list of CLI command in BASH in Ubuntu 20.04.
    It depends on built-in Python library, well, right now.

    Args:
        commands (list): a list of commands, e.g., echo "Hello, World!"
        separate_terminal: open up a new terminal and run the commands there.
    Return:
        An execution status (integer), which should be 0 on success. (TBC)
    """

    import subprocess as sp

    try:
        if separate_terminal:
            return sp.run(["x-terminal-emulator", "-e", " && ".join(str(command) for command in commands)], shell=False, check=True)
        else:
            return sp.run(commands, shell=True, check=True)
    except sp.CalledProcessError as e:
        print(f"[subprocess] Couldn't execute command: {e}")
        return -1 # ERROR!
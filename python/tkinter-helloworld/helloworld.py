from tkinter import *

# In tkinter everything is "Widget".
root = Tk()

# All Widgets are defined in 2-steps.
# First, we define Widget.
label = Label(root, text="Hello World!")

# Second we render Widget.
label.pack()

# To show window, we enter main loop.
root.mainloop()
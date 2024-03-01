import tkinter as tk

# Secondary dependencies:
import PIL.Image as pimage
import PIL.ImageTk as pimagetk

# In tkinter everything is "Widget".
root = tk.Tk()

# Application configuration.
root.title("A simple tool to view an image.")
root.maxsize(850, 430)
root.config(bg="black")

# Side panel.
frame_side = tk.Frame(root, bg="green")
frame_side.grid(row=0, column=0, padx=5, pady=5)

# Main panel.
frame_main = tk.Frame(root, bg="#bbb")
frame_main.grid(row=0, column=1, padx=5, pady=5)


frame_tool = tk.Frame(frame_side, width=400, height=200, bg="red")
frame_tool.grid(row=0, column=0, padx=5, pady=5)

frame_info = tk.Frame(frame_side, width=400, height=200, bg="purple")
frame_info.grid(row=1, column=0, padx=5, pady=5)

frame_view = tk.Frame(frame_main,  bg="blue") # width=410, height=410,
frame_view.grid(row=0, column=0, padx=5, pady=5)

# Load image.
image = pimagetk.PhotoImage(pimage.open("logo.png").resize((410, 410)))
image_view = tk.Label(frame_view, image = image)
image_view.pack(fill=tk.BOTH, expand=1)

# To show window, we enter the main loop.
root.mainloop()


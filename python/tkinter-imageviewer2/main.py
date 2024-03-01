import tkinter as tk
import tkinter.ttk as ttk
import tkinter.filedialog as filedialog

# Secondary dependencies:
import PIL.Image as pimage
import PIL.ImageTk as pimagetk

# https://stackoverflow.com/questions/56043767/show-large-image-using-scrollbar-in-python
class ScrollableImage(tk.Frame):
    def __init__(self, master=None, **kw):
        self.image = kw.pop('image', None)
        sw = kw.pop('scrollbarwidth', 10)
        super(ScrollableImage, self).__init__(master=master, **kw)
        self.cnvs = tk.Canvas(self, highlightthickness=0, **kw)
        self.cnvs.create_image(0, 0, anchor='nw', image=self.image)
        # Vertical and Horizontal scrollbars
        self.v_scroll = tk.Scrollbar(self, orient='vertical', width=sw)
        self.h_scroll = tk.Scrollbar(self, orient='horizontal', width=sw)
        # Grid and configure weight.
        self.cnvs.grid(row=0, column=0,  sticky='nsew')
        self.h_scroll.grid(row=1, column=0, sticky='ew')
        self.v_scroll.grid(row=0, column=1, sticky='ns')
        self.rowconfigure(0, weight=1)
        self.columnconfigure(0, weight=1)
        # Set the scrollbars to the canvas
        self.cnvs.config(xscrollcommand=self.h_scroll.set, 
                           yscrollcommand=self.v_scroll.set)
        # Set canvas view to the scrollbars
        self.v_scroll.config(command=self.cnvs.yview)
        self.h_scroll.config(command=self.cnvs.xview)
        # Assign the region to be scrolled 
        self.cnvs.config(scrollregion=self.cnvs.bbox('all'))
        self.cnvs.bind_class(self.cnvs, "<MouseWheel>", self.mouse_scroll)
        self.cnvs.bind('<Button-4>', self.mouse_scroll_down)
        self.cnvs.bind('<Button-5>', self.mouse_scroll_up)

    def mouse_scroll(self, evt):
        if evt.state == 0 :
            self.cnvs.yview_scroll(-1*(evt.delta), 'units') # For MacOS
            self.cnvs.yview_scroll(int(-1*(evt.delta/120)), 'units') # For windows
        if evt.state == 1:
            self.cnvs.xview_scroll(-1*(evt.delta), 'units') # For MacOS
            self.cnvs.xview_scroll(int(-1*(evt.delta/120)), 'units') # For windows

    def mouse_scroll_down(self, evt):
        if evt.state == 16:
            self.cnvs.yview_scroll(int(-1*evt.num), 'units') # For Linux
        elif evt.state == 17:
            self.cnvs.xview_scroll(int(-1*evt.num), 'units') # For Linux

    def mouse_scroll_up(self, evt):
        if evt.state == 16:
            self.cnvs.yview_scroll(int(evt.num), 'units') # For Linux
        elif evt.state == 17:
            self.cnvs.xview_scroll(int(evt.num), 'units') # For Linux



def open_file():

    # Show a pop up window to let user select the image
    # to open in a new window.
    filename : str = filedialog.askopenfilename()

    # Create the new window.
    new_window = tk.Toplevel(root)

    # Configure the new window.
    new_window.title(filename)
    new_window.resizable(False, False)

    # This is the image loading process.
    image = pimagetk.PhotoImage(pimage.open(filename))
    image_view = ScrollableImage(new_window, image=image, scrollbarwidth=16, 
                               width=512, height=512)
    image_view.pack()

    # Without this line of code, the image won't show up.
    new_window.mainloop()

# In tkinter everything is "Widget".
root = tk.Tk()

# Application configuration.
root.title("A simple tool to view an image.")

# Menu.
menu = tk.Menu(root)
file = tk.Menu(menu, tearoff=0)

menu.add_cascade(label="File", menu=file)

file.add_command(label="Open...", command=open_file)
file.add_command(label="Exit", command=root.destroy)

button_open_image = ttk.Button(root, text="Open an image", command=open_file)
button_open_image.pack()

root.config(menu=menu)

# To show window, we enter main loop.
root.mainloop()
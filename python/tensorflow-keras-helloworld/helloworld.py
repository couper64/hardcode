import os         as os
# The environment variable TF_CPP_MIN_LOG_LEVEL adjusts the verbosity of the
# Tensorflow package with the following values:
#    0 = all messages are logged (default behavior)
#    1 = INFO messages are not printed
#    2 = INFO and WARNING messages are not printed
#    3 = INFO, WARNING, and ERROR messages are not printed
# The line above sets the environment varaible from within the script.
#    os.environ["TF_CPP_MIN_LOG_LEVEL=1"] = 0
# Otherwise, execute the script in the Bash shell a similar manner to this,
#    clear; TF_CPP_MIN_LOG_LEVEL=1 python helloworld.py
# because the shell syntax describes this as being functionally equivalent to:
#    export TF_CPP_MIN_LOG_LEVEL=1
#    python helloworld.py
#    unset TF_CPP_MIN_LOG_LEVEL
import tensorflow        as tf
import numpy             as np
import matplotlib.pyplot as plt

# To get the version of the installed package of Tensorflow
# we can use the internal variable __version__.
print(tf.__version__)

# The Getting Started example uses MNIST dataset for classification,
# we are going to use it as well.
mnist = tf.keras.datasets.mnist

# The MNIST module has only 1 function .load_data(), and returns 
# a tuple of 2 tuples.
# 
# From the documentation, the x_train is a NumPy array
# 28x28 images, of shape (60000, 28, 28), of type uint8, of range 0-255.
# The y_train is a NumPy array of labels, of shape (60000,), of type uint8,
# of range 0-255.
# 
# From the documentation, the same happens to the x_test and y_test, but
# from 60000 we have only 10000 images and labels.
(x_train, y_train), (x_test, y_test) = mnist.load_data()

# We have to convert the RGB values from range of 0 to 255, to 
# range of 0 to 1.
# For yet unknown to me reasons, we don't do the same for the test data.
x_train, x_test = x_train / 255.0, x_test / 255.0

# Here, we do a very simple Sequantila model, however, it is more common to 
# develop a Functional API model, but I will get back to it later.
# The input shape should match the size of the image.
# I imagine, the Dense layer create 128 neurons and connects all inputs to
# neurons and all neurons to outputs.
# The Dropout layer is used to prevent overfitting by randomly setting some of
# the inputs to zero.
model : tf.keras.models.Sequential = tf.keras.models.Sequential\
([
    tf.keras.layers.Flatten(input_shape=(28, 28)                   ),
    tf.keras.layers.Dense  (128                 , activation="relu"),
    tf.keras.layers.Dropout(0.2                                    ),
    tf.keras.layers.Dense  (10                                     )
])

# By calling the model, we run a through the forward pass.
# At the end we receive a Tensor of Rank 2 (list of lists),
# so we want to conver to a numpy array, we can either 
# do tensor.numpy() or np.array(tensor).
# I prefer the second way, because this way the code editor could pick it up
# and help me debug it.
print(np.array(model(x_train[:1])))

# In order to convert the prediction logits to prediction values,
# we run the results through an activation function such as softmax.
# It should bring the values to the range between 0 and 1.
print(np.array(tf.nn.softmax(np.array(model(x_train[:1])))))

# For the training, we need to define a loss function, in order to evaluate
# the model and find the version which has already reached the min on 
# the gradient.
loss_fn : tf.keras.losses.SparseCategoricalCrossentropy = \
(
    tf.keras.losses.SparseCategoricalCrossentropy(from_logits=True)
)
print(loss_fn(y_train[:1], np.array(model(x_train[:1]))))

# Before the training, we must compile the model, and optinally, configure.
model.compile(optimizer="adam", loss=loss_fn, metrics=["accuracy"])

# Now, this is training.
model.fit(x_train, y_train, epochs=5)

# And, here is the validation.
model.evaluate(x_test, y_test, verbose=2)

# Later, if we want to use the model for inference (demonstration),
# we need to wrap the model to pass the values of the last layer
# through an activatin function to have values in range from 0 to 1,
# similar to what we did earlier when we were converting the logits to 
# prediction values.
probability_model : tf.keras.Sequential = tf.keras.Sequential\
([
    model                    ,
    tf.keras.layers.Softmax()
])
# This bit of the code is optional, it is required to print the values
# of the array without scientific notation.
np.set_printoptions(suppress=True)

# Here we actually printing the prediction values in the following order:
# 0->0, 1->1, 2->2, 3->3, 4->4, 5->5, 6->6, 7->7, 8->8, 9->9
print(np.array(probability_model(x_test[1:2])))

# Here is just a little representation of what we are trying to 
# classify.
plt.imshow(x_test[1], cmap='Greys_r')
plt.show()

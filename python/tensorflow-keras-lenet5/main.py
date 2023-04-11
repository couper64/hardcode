import tensorflow as tf
import numpy      as np

# Start creating a model using Functional API.
x_input : tf.Tensor = tf.keras.layers.Input(shape=(32, 32, 1))

x : tf.Tensor = x_input

x = tf.keras.layers.Conv2D          (filters   = 6  , kernel_size = 5, strides = 1, activation = "relu", input_shape = (28, 28, 1 ))(x_input)
x = tf.keras.layers.AveragePooling2D(pool_size = 2  , strides     = 2                                                              )(x)
x = tf.keras.layers.Conv2D          (filters   = 16 , kernel_size = 5, strides = 1, activation = "relu", input_shape = (14, 14, 6 ))(x)
x = tf.keras.layers.AveragePooling2D(pool_size = 2  , strides     = 2                                                              )(x)
x = tf.keras.layers.Conv2D          (filters   = 120, kernel_size = 1, strides = 1, activation = "relu", input_shape = (5 , 5 , 16))(x)
x = tf.keras.layers.Flatten         (                                                                                              )(x)
x = tf.keras.layers.Dense           (units     = 84 , activation  = "relu"                                                         )(x)
x = tf.keras.layers.Dense           (units     = 10 , activation  = "softmax"                                                      )(x)

x_output : tf.Tensor = x

model : tf.keras.Model = tf.keras.Model(inputs=x_input, outputs=x_output, name="LeNet-5")

# Loading Modified NIST dataset.
(x_train, y_train), (x_test, y_test) = tf.keras.datasets.mnist.load_data()

# For our purposes, we convert it to the [0, 1] range.
x_train = x_train / 255.0
x_test  = x_test  / 255.0

# Also, we want to expand the image up to 32x32 as per the paper.
x_train = np.expand_dims(x_train, -1)
x_test  = np.expand_dims(x_test , -1)

x_train = np.pad(x_train, ((0, 0), (2, 2), (2, 2), (0, 0)), "constant")
x_test  = np.pad(x_test , ((0, 0), (2, 2), (2, 2), (0, 0)), "constant")

model.summary()

model.compile (optimizer=tf.keras.optimizers.Adam(), loss=tf.keras.losses.SparseCategoricalCrossentropy(), metrics=["accuracy"]          )
model.fit     (x=x_train                           , y=y_train                                           , batch_size=10       , epochs=8)
model.evaluate(x=x_test                            , y=y_test                                                                            )

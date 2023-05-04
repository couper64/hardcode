import streamlit as st

# Initialization
if "key" not in st.session_state:
    st.session_state["key"] = "value"

# Read
st.write(st.session_state.key)

# Outputs: value

# Delete a single key-value pair
del st.session_state["key"]

st.text_input("Your name", key="name")

# This exists now:
print(st.session_state.name)
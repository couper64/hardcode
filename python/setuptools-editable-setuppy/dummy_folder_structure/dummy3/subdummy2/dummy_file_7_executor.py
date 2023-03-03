import tqdm as tqdm

import dummy_folder_structure.dummy1.subdummy1.dummy_file_1 as d1_sd1_df1
import dummy_folder_structure.dummy1.subdummy1.dummy_file_2 as d1_sd1_df2
import dummy_folder_structure.dummy1.subdummy2.dummy_file_8 as d1_sd2_df8
import dummy_folder_structure.dummy1.subdummy2.dummy_file_9 as d1_sd2_df9

import dummy_folder_structure.dummy2.subdummy1.dummy_file_3 as d2_sd1_df3
import dummy_folder_structure.dummy2.subdummy2.dummy_file_4 as d2_sd2_df4
import dummy_folder_structure.dummy2.subdummy3.dummy_file_5 as d2_sd3_df5

import dummy_folder_structure.dummy3.subdummy1.dummy_file_6 as d3_sd1_df6


[i for i in tqdm.tqdm(d1_sd1_df1.POWERRANGERS  )]
[i for i in tqdm.tqdm(d1_sd1_df2.PUFFPUFFGIRLS )]
[i for i in tqdm.tqdm(d1_sd2_df8.XMEN          )]
[i for i in tqdm.tqdm(d1_sd2_df9.IBM_MODEL_LINE)]

[i for i in tqdm.tqdm(d2_sd1_df3.PYTHON_VERSIONS )]
[i for i in tqdm.tqdm(d2_sd2_df4.POKEMON_TIMELINE)]
[i for i in tqdm.tqdm(d2_sd3_df5.DND_LINEAGES    )]

[i for i in tqdm.tqdm(d3_sd1_df6.DND_EXOTIC_LINEAGES)]
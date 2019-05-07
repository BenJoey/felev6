#include <iostream>
#include <string>
#include <sstream>
#include <map>

enum type {natural, boolean};

struct var_data{
    int decl_row;
    type var_type;
    
    var_data(int row, type v_t):decl_row(row), var_type(v_t) {}
    var_data() {}
};
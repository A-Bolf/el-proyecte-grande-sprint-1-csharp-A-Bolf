import React from "react";
import { Select, MenuItem } from "@mui/material";

const Dropdown = ({ label, options, toChange, baseValue }) => {
  return (
    <>
      {label}
      <Select
        style={{ margin: "25px" }}
        variant="standard"
        autoWidth
        defaultValue={baseValue || options[0]}
        onChange={(e) => {
          toChange(e.target.value);
        }}
      >
        {options.map((option) => (
          <MenuItem value={option}>{option}</MenuItem>
        ))}
      </Select>
    </>
  );
};

export default Dropdown;

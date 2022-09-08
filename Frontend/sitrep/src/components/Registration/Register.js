import * as React from "react";
import Button from "@mui/material/Button";
import CssBaseline from "@mui/material/CssBaseline";
import TextField from "@mui/material/TextField";
import FormControlLabel from "@mui/material/FormControlLabel";
import Checkbox from "@mui/material/Checkbox";
import Grid from "@mui/material/Grid";
import Box from "@mui/material/Box";
import Container from "@mui/material/Container";
import ErrorAlert from "../Alerts/ErrorAlert";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import { useContext } from "react";
import AuthContext from "../Context/AuthProvider";

const theme = createTheme({
  palette: {
    primary: { main: "#343a40" },
  },
});

const SignUp = () => {
  const { registerFetch, errMsg, setErrMsg } = useContext(AuthContext);

  const handleSubmit = (event) => {
    event.preventDefault();
    const data = new FormData(event.currentTarget);
    registerFetch(data);
    console.log({
      username: data.get("username"),
      password: data.get("password"),
      pass2: data.get("password-confirm"),
    });
  };

  return (
    <ThemeProvider theme={theme}>
      <Container component="main" className={"registration"} maxWidth="xs">
        <CssBaseline />
        <Box
          sx={{
            display: "flex",
            flexDirection: "column",
            alignItems: "center",
          }}
        >
          <Box
            component="form"
            noValidate
            onSubmit={handleSubmit}
            onChange={() => {
              setErrMsg("");
            }}
            sx={{ mt: 3 }}
          >
            <Grid container spacing={2}>
              <Grid item xs={12}>
                <TextField
                  required
                  fullWidth
                  id="username"
                  label="Username"
                  name="username"
                  autoComplete="off"
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  required
                  fullWidth
                  name="password"
                  label="Password"
                  type="password"
                  id="password"
                  autoComplete="new-password"
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  required
                  fullWidth
                  name="password-confirm"
                  label="Confirm Password"
                  type="password"
                  id="password-confirm"
                />
              </Grid>
              <Grid item xs={12}>
                <FormControlLabel
                  control={
                    <Checkbox value="allowDataSelling" color="primary" />
                  }
                  label="I consent to my private data being sold to ad companies."
                />
              </Grid>
              <ErrorAlert text={errMsg} open={errMsg} />
              <Button
                type="submit"
                fullWidth
                variant="contained"
                sx={{ mt: 3, mb: 2 }}
              >
                Sign Up
              </Button>
              <Grid container justifyContent="flex-end">
                <Grid item></Grid>
              </Grid>
            </Grid>
          </Box>
        </Box>
      </Container>
    </ThemeProvider>
  );
};
export default SignUp;

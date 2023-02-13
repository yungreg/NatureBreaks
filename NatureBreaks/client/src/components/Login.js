import React, { useState } from "react";
import { Button, Form, FormGroup, Label, Input } from "reactstrap";
import { useNavigate, Link } from "react-router-dom";
import { login } from "../modules/authManager";

export default function Login() {
  let navigate = useNavigate();

  const [email, setEmail] = useState();
  const [password, setPassword] = useState();

  const loginSubmit = (e) => {
    e.preventDefault();
    login(email, password)
      .then(() => navigate("/"))
      .catch(() => alert("Login Failed"));
  };

  return (
    <Form onSubmit={loginSubmit}>
      <fieldset>
        <FormGroup>
          <Label for="email"></Label>
          <Input
            id="email"
            type="text"
            placeholder="Email?"
            autoFocus
            onChange={(e) => setEmail(e.target.value)}
          />
        </FormGroup>
        <FormGroup>
          <Label for="password"></Label>
          <Input
            id="password"
            type="password"
            placeholder="Password?"
            onChange={(e) => setPassword(e.target.value)}
          />
        </FormGroup>
        <FormGroup>
          <Button>Login</Button>
        </FormGroup>
        <em>
          Not a Break Taker yet?
          <Link to="/register">Become a Break Taker here!</Link>
        </em>
      </fieldset>
    </Form>
  );
}

/*
todo: edit this page to be teh Video uPLload form for an admin user
todo: create a state for nature types to be shown in the upload form's select box 
todo: create a user for nature types to be shown in the upload form's select box 
todo: figure out map fn to map through select box chocies
todo: enable save button properly
*/

import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Button, Form, FormGroup, Label, Input } from "reactstrap";
import { addVideo } from "../modules/videoManager";
import { VideoList } from "./VideoList";

export default function VideoAddForm() {
  const navigate = useNavigate();
  const [video, setVideo] = useState();

  //*hold and observe NatureType State
  const [natureType, setNatureType] = useState([]);
  useEffect(() => {
    fetch(`https://localhost:5001/api/naturetype`)
      .then((res) => res.json())
      .then((input) => {
        const natureTypeOption = input;
        setNatureType(natureTypeOption);
      });
  }, []);

  //todo: hold and observe User State
  const [user, setUser] = useState([]);
  useEffect(() => {
    fetch(`https://localhost:5001/api/user`)
      .then((res) => res.json())
      .then((input) => {
        const userFirstNames = input;
        setUser(userFirstNames);
      });
  }, []); //i think this is incorrect to get the map going

  const submitForm = (video) => {
    // e.preventDefault();
    addVideo({ video })
      .then(() => navigate("/"))
      .catch((err) => alert(`An error ocurred: ${err.message}`));
  };

  return (
    <Form onSubmit={submitForm}>
      <FormGroup>
        <Label for="videoName">What's the Break's name? </Label>
        <Input
          id="videoName"
          type="textarea"
          placeholder="Format: 'Nature Breaks = Nature Type 00X'"
          onChange={(e) => setVideo(e.target.videoName)}
        />
      </FormGroup>
      <FormGroup>
        <Label for="videoSeason">Season? </Label>
        <Input
          id="videoSeason"
          type="textarea"
          placeholder="What season was the Break? ex: 'Fall' or 'Winter'"
          onChange={(e) => setVideo(e.target.season)}
        />
      </FormGroup>
      <FormGroup>
        <Label for="videoUrl">Video Link?</Label>
        <Input
          id="videoUrl"
          type="textarea"
          placeholder="Note: be sure to use an EMBED link!"
          onChange={(e) => setVideo(e.target.videoUrl)}
        />
      </FormGroup>
      {/* Select box for NatureTypes */}
      <FormGroup>
        <Label for="natureTypeSelect">Select A Nature Type: </Label>
        <Input type="select" name="select" id="natureTypeSelect">
          {natureType.map((type) => (
            <option key={type.id} value={type.id}>
              {type.natureTypeName}
            </option>
          ))}
        </Input>
      </FormGroup>
      {/* picking a user */}
      {/* 
//todo: create a map fn for this
*/}
      <FormGroup>
        <Label for="userSelect">Who's making this Break? </Label>
        <Input type="select" name="select" id="userSelect">
          {user.map((person) => (
            <option key={person.id} value={person.id}>
              {person.firstName}
            </option>
          ))}
        </Input>
      </FormGroup>
      <FormGroup>
        <Label for="videoCity">Closest Major City?</Label>
        <Input
          id="videoCity"
          type="textarea"
          placeholder="What's the closest major city to the Break?"
          onChange={(e) => setVideo(e.target.ClosestMajorCity)}
        />
      </FormGroup>
      <FormGroup>
        <Button>Save</Button>
      </FormGroup>
    </Form>
  );
}

{
  /* <FormGroup>
<Label for="natureTypeSelect">Select A Nature Type:</Label>
<Input id="natureTypeSelect" name="select" type="select">
  <option>Moving Water</option>
  <option>Mountains</option>
  <option>Rainfall</option>
  <option>Tree Talk</option>
  <option>Woodland</option>
  <option>Snow</option>
  <option>Ice</option>
  <option>Wildlife</option>
  <option>Beach</option>
</Input>
</FormGroup> */
}

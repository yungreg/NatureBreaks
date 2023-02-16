/*
todo: edit this page to be teh Video edit form for an admin user
*todo: create a state for nature types to be shown in the upload form's dropdown box 
*todo: create a user for nature types to be shown in the upload form's select box 
*todo: figure out map fn to map through select box chocies
todo: enable save button properly
*/

import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Button, Form, FormGroup, Label, Input } from "reactstrap";
import { getVideoById, addVideo } from "../modules/videoManager";
import { useParams } from "react-router-dom";

export default function VideoEditor() {
  const navigate = useNavigate();

  const [video, setVideo] = useState({
    season: "",
    natureTypeId: 1,
    userId: 1,
    closestMajorCity: "",
    videoName: "",
    videoUrl: "",
  });
  const { videoId } = useParams();

  // const [userChoices, setUserChoices] = useState({});

  //*hold and observe NatureType State for select box
  const [natureType, setNatureType] = useState([]);
  useEffect(() => {
    fetch(`https://localhost:5001/api/naturetype`)
      .then((res) => res.json())
      .then((input) => {
        const natureTypeOption = input;
        setNatureType(natureTypeOption);
      });
  }, []);

  //* todo: hold and observe User State
  const [user, setUser] = useState([]);
  useEffect(() => {
    fetch(`https://localhost:5001/api/user`)
      .then((res) => res.json())
      .then((input) => {
        const userFirstNames = input;
        setUser(userFirstNames);
      });
  }, []);

  //* todo for video State
  useEffect(() => {
    getVideoById(videoId).then((video) => setVideo(video));
  }, []);

  const handleInputChange = (event) => {
    const copy = { ...video };
    copy[event.target.id] = event.target.value;
    setVideo(copy);
  };

  const handleEditButtonClick = (clickEvent) => {
    clickEvent.preventDefault();
    // const userChoices = {
    //   season: video.season,
    //   natureTypeId: video.natureTypeId,
    //   userId: video.userId,
    //   closestMajorCity: video.closestMajorCity,
    //   videoName: video.videoName,
    //   videoUrl: video.videoUrl,
    // };

    // if (
    //   video.season &&
    //   video.natureTypeId &&
    //   video.userId &&
    //   video.closestMajorCity &&
    //   video.videoName &&
    //   video.videoUrl
    // ) {
    fetch(`/api/video/${videoId}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(video),
    })
      .then(() => {
        navigate("/");
      })
      .catch((err) =>
        alert(`I couldn't work: ${err.message}`).then(() => {
          window.alert("Break updated! Back to your favorites...");
        })
      );
  };

  return (
    <Form>
      <FormGroup>
        <h2 className="videoEditor__Title">Update Your Nature Break!</h2>
        <Label for="videoName">What's the Break's new name? </Label>
        <Input
          id="videoName"
          type="textarea"
          value={video.videoName}
          placeholder="Format: 'Nature Breaks = Nature Type 00X'"
          onChange={handleInputChange}
        />
      </FormGroup>

      <FormGroup>
        <Label for="videoUrl">Is the Video's URL the same?</Label>
        <Input
          id="videoUrl"
          type="textarea"
          value={video.videoUrl}
          placeholder="Note: be sure to use an EMBED link!"
          onChange={handleInputChange}
        />
      </FormGroup>
      {/* Select box for NatureTypes */}
      <FormGroup>
        <Label for="natureTypeSelect">
          Select A Nature Type for this video:{" "}
        </Label>
        <Input
          onChange={handleInputChange}
          type="select"
          value={video.natureTypeId}
          name="select"
          id="natureTypeId"
        >
          {natureType.map((type) => (
            <option key={type.id} value={type.id}>
              {type.natureTypeName}
            </option>
          ))}
        </Input>
      </FormGroup>
      {/* todo: create a map fn for this */}
      <FormGroup>
        <Label for="userSelect">Who's making this Break? </Label>
        <Input
          onChange={handleInputChange}
          type="select"
          value={video.userId}
          name="select"
          id="userId"
        >
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
          id="closestMajorCity"
          type="text"
          value={video.closestMajorCity}
          placeholder="What's the closest major city to the Break?"
          onChange={handleInputChange}
        />
      </FormGroup>
      <FormGroup>
        <Button
          color="warning"
          outline
          onClick={(clickEvent) => handleEditButtonClick(clickEvent)}
        >
          Change & Save This Break?
        </Button>
      </FormGroup>
    </Form>
  );
}

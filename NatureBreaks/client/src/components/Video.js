import React from "react";
import { Link } from "react-router-dom";
import { Card, CardBody } from "reactstrap";
import VideoEditor from "./VideoEditor.js";

/*
todo: make a button here that allows you to save a video to a profile
*/

const Video = ({ video }) => {
  const deleteButton = () => {
    return (
      <button
        className="session__btn-delete"
        onClick={() => {
          fetch(`https://localhost:5001/api/video/${video.id}`, {
            method: "DELETE",
          })
            .then((response) => response.json)
            .then(() => {
              window.alert("Break deleted! Thanks!");
            });
        }}
      >
        Delete This Break?
      </button>
    );
  };
  return (
    <Card>
      <Link to={`/videoeditor/${video.id}`}>
        {/* link will go here that takes you to the break page for deleting */}
        <strong>{video.videoName}</strong>
      </Link>
      <CardBody>
        <iframe
          className="video"
          width="560"
          height="315"
          src={video.videoUrl}
          title="YouTube video player"
          frameborder="0"
          allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share"
          allowfullscreen
        ></iframe>
        {deleteButton()}
        {VideoEditor()}
      </CardBody>
    </Card>
  );
};

export default Video;

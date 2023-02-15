import React from "react";
import { Card, CardBody } from "reactstrap";

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
      <p>
        {/* link will go here that takes yout o tthe break page for deleting */}
        <strong>{video.videoName}</strong>
      </p>
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
      </CardBody>
    </Card>
  );
};

export default Video;

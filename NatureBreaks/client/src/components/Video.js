import React from "react";
import { Card, CardBody } from "reactstrap";

/*
todo: make a button here that allows you to save a video to a profile
*/

const Video = ({ video }) => {
  return (
    <Card>
      <p>
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
      </CardBody>
    </Card>
  );
};

export default Video;

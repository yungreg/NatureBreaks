import { getToken } from "./authManager";

// thsi will set the homepge url
const baseUrl = "/api/video";

export const getAllVideos = () => {
  return fetch(baseUrl).then((res) => res.json());
};

export const addVideo = (vid) => {
  return fetch(baseUrl, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(vid),
  });
};

export const getVideoById = (videoId) => {
  return getToken().then((token) => {
    return fetch(`${baseUrl}/${videoId}`, {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`,
      },
    }).then((resp) => {
      if (resp.ok) {
        return resp.json();
      } else {
        throw new Error(
          "An unknown error occurred while trying to update ths video :/"
        );
      }
    });
  });
};

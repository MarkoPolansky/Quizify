function generateNewUuid( cvalue, exdays) {
    const d = new Date();
    d.setTime(d.getTime() + (exdays*24*60*60*1000));
    let expires = "expires="+ d.toUTCString();
    document.cookie =   "generated_uuid=" + crypto.randomUUID() + ";" + expires + ";path=/";
}


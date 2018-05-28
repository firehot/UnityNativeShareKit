struct AlertMessageStruct {
	char* alertTitle;
	char* alertMessage;
	char* alertCancelButtonText;
};

#ifdef __cplusplus
extern "C" {
#endif
	void showAlertMessage(struct AlertMessageStruct *alertStruct);
#ifdef __cplusplus
}
#endif

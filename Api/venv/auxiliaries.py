import os
import hashlib
import base64

# function that encodes password. Self explanatory
def encodePassword(password):
    encoded_str = password.encode()
    sha_3_encoded = hashlib.sha3_256(encoded_str)
    return sha_3_encoded.hexdigest()


#This function creates the database file if it doesnt exist, and it also intialises andy uninitialised tables in the database file
def init_db(db):
    db.create_all()
    if not os.path.isfile('./sqlite3.db'):
        print("hello")
        db.create_all()
        print("Database did not exist, created")


#This function checks if the a login
def checkValidUsersRequest(data):
    # print('user' in data and 'password' in data)
    if not ('user' in data and 'password' in data):
        return {'error':"the json format is incorrect"}
    
def createToken(data):
    toBeEncoded = str(data['user']+'////'+encodePassword(data['password'])).encode("utf-8")
    encoded = base64.b64encode(toBeEncoded)
    encoded = encoded.decode("utf-8")
    print(encoded)
    return encoded
def decodeToken(token):
    temp=token.encode('utf-8')
    decoded=base64.decodebytes(temp)
    values=decoded.decode().split('////')
    return values
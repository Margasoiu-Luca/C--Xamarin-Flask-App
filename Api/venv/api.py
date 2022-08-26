from flask import request, json, Response, jsonify
from models import *
from auxiliaries import *
from flask_cors import cross_origin
#Cors is used for cross-origin applications 
#Without including it there would be errors when running the applications


init_db(db)

#This method verifies if the a user exists with the a specific username and password

def attemptLogin(data):
    print(data['user'],data['password'])
    x = User.query.filter_by(username = data['user'],password = data['password']).all()
    print(x)
    print(x==1)
    if len(x)==1:
        return True
    return False

@app.route("/api/test")
def helloWorld():
  return "Hello, cross-origin-world!"


#This method attemps to validate a token by:
#1. Decoding it from base64 
#2. Attemps to log in with the values from base64 string. Returns true if ok false if not
@app.route("/api/validateToken", methods=['POST'])
def validate():
    data = request.get_json()
    print(data['token'])
    values=decodeToken(data['token'])
    valid= attemptLogin({"user":values[0],"password":values[1]})
    return str(valid)

#Method to create a user
@app.route('/api/register',methods = ['POST', 'GET'])
@cross_origin()
def register():
    #Stop if method is get
    if request.method == 'GET':
        return {'error':'Method Not avalabile'}
    elif request.method == 'POST':
        request_data = request.get_json()
        #Check if request_data has 'user' and 'password'
        if checkValidUsersRequest(request_data):
            return Response(json.dumps({'error':'the json format is incorrect'}),status=400)
        try:
            user = User(username = request_data['user'],password = encodePassword(request_data['password']))
            db.session.add(user)
            db.session.commit() 
        except Exception as e :
            d=json.dumps({"error":str(e)})
            print(d)
            return Response(d, status=500)
        return Response(json.dumps({'successful':'account added successfully'}),status=201)
  

@app.route('/api/user/<user>', methods = ['DELETE'])
@cross_origin()
def delUsr(user):
    try:
        x = User.query.filter_by(username = user).all()
        db.session.commit()
        if len(x)==0:
            return Response(json.dumps({'bad request':'User does not exist'}),status=400)
        else:
            User.query.filter_by(username=user).delete()
            db.session.commit()
            Food.query.filter_by(user_name=user).delete()
            db.session.commit()
            return Response(json.dumps({'sucessful':'User removed successfully'}),status=200)

    except Exception as e:
        d=json.dumps({"error":str(e)})
        print(d)
        return Response(d, status=401)




#Method to login
@app.route('/api/login',methods = ['POST'])
@cross_origin()
def login():
    data = request.get_json()
    if checkValidUsersRequest(data):
        return Response(json.dumps({'error':"the json format is incorrect"}),status=400)
    encryptedPass = encodePassword(data['password'])
    if not attemptLogin({"user":data['user'],"password":encryptedPass}):
        return Response(json.dumps({'unauthorised':'incorrect credentials'}),status=401)
    token = createToken(data)
    return Response(json.dumps({'token':str(token)}),status=200)


#Method to get all food items from user
@app.route('/api/user/food', methods =['GET'])
@cross_origin()
def getFoodForUser():
    auth = request.headers.get('Authorization')
    try:
        values = decodeToken(auth)
        valid = attemptLogin({"user":values[0],"password":values[1]})
        search = Food.query.filter_by(user_name = values[0]).all()
        print(search)
        if not search:
            return Response(json.dumps({'no content':'no foods found for username'}),status=206)
        return jsonify(search)
    except Exception as e :
        d=json.dumps({"error":str(e)})
        print(d)    
        return Response(d, status=500)


#Method to get food from user.
@app.route('/api/food', methods = ['GET', 'POST'])
@cross_origin()
def addFood():
    data = request.get_json()
    auth = request.headers.get('Authorization')
    try:
        values = decodeToken(auth)
        print(values[0])
        valid = attemptLogin({"user":values[0],"password":values[1]})
        if(valid == False):
            return Response(json.dumps({'unauthorized':'non-valid authorization header'}),status=401)
    except Exception as e:
        d=json.dumps({"error":str(e)})
        print(d)
        return Response(d, status=401)
    try:
        if request.method == 'POST':
            food = Food(id=data['id'],name=data['name'],description=data['description'],vegan=data['vegan'],imageLink=data['imageLink'], calories=data['calories'], user_name=values[0])
            db.session.add(food)
            db.session.commit()
            return Response(json.dumps({'sucessful':'Food added successfully'}),status=201)
    except Exception as e :
        d=json.dumps({"error":str(e)})
        print(d)
        return Response(d, status=500)


@app.route('/api/food/<food_id>', methods = ['DELETE'])
@cross_origin()
def data(food_id):
    assert food_id == request.view_args['food_id']
    try:
        Food.query.filter_by(id=food_id).delete()
        db.session.commit()
        return Response(json.dumps({'sucessful':'Food removed successfully'}),status=200)
    except Exception as e:
        d=json.dumps({"error":str(e)})
        print(d)
        return Response(d, status=401)


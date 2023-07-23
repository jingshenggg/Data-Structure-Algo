# #removing dulplicate in sorted 
# list = [1,1,2,3,4,5,5,6,8,9,10,11,11,11]
# # result = []
# # result.append(list[0])
# # # print(result)

# # def remove():
# #     temp = list[0]
# #     for i in range(1,len(list)):
# #         if temp != list[i]:
# #             temp = list[i]
# #             result.append(list[i])
# # remove()
# # print(result)

# k = []
# k.append(list[0])
# # print(k)
# temp = list[0]

# for i in range (1, len(list)):
#     if temp != list[i]:
#         temp = list[i]
#         k.append(list[i])
# print(k)


# haystack = "sadbutsad"
# needle = "sad"
# print (len(needle))
# for j in range(len(needle)):
#     print (needle[j])
# for i in range(0,len(needle)):
#     print (needle[i])

import hashlib

def sha256_hash(input_string):
    # Create a SHA-256 hash object
    sha256 = hashlib.sha256()

    # Convert the input string to bytes
    input_bytes = input_string.encode('utf-8')

    # Update the hash object with the input bytes
    sha256.update(input_bytes)

    # Get the hexadecimal representation of the hash value
    hashed_value = sha256.hexdigest()

    return hashed_value

# Example usage
plaintext = "Hello, World!"
hashed_value = sha256_hash(plaintext)
print("Plaintext:", plaintext)
print("SHA-256 Hash:", hashed_value)